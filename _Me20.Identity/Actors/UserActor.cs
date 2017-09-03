using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using Me20.Common.CosmosDB;
using Me20.Common.Extensions;
using Me20.Identity.Abstracts;
using Me20.Identity.Commands;
using Me20.Identity.Events;
using Me20.Identity.Models;
using Me20.Identity.QueryMessages;
using Me20.Identity.QueryResultMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Me20.Identity.Actors
{
    //TODO: Make it persistent actor
    public class UserActor : ReceiveActorBase
    {
        private UserActorState ActorState { get; set; }

        public UserActor(string authenthicationType, string id) : base()
        {
            ActorState = new UserActorState(authenthicationType, id);

            RegisterCommands();

            RegisterQueries();
        }

        //TODO: Some kind of collection that registers this stuff in foreach?
        private void RegisterCommands()
        {
            //Logging in persisted at 15 mins
            Receive<UserLoggedInCommand>(cmd => HandleUserLoggedInMessageAsync(cmd));
            //Tag subscribtion
            Receive<SubscribeToTagCommand>(cmd => HandleAddSubscribedTagCommand(cmd));

            //Content added by user
            Receive<AddContentCommand>(cmd => HandleAddContentCommand(cmd));
            //Content tagged by user
            Receive<TagContentCommand>(cmd => HandleTagContentCommand(cmd));

            //Content rated by user
            Receive<RateContentCommand>(cmd => HandleRateContentCommand(cmd));

            Receive<RemoveUserContentCommand>(cmd => HandleRemoveUserContentCommand(cmd));

            Receive<RenameUserContentCommand>(cmd => HandleRenameUserContentCommand(cmd));
        }

        private void RegisterQueries()
        {
            Receive<GetAllTagNamesForUserQueryMessage>(msg => Sender.Tell(new GetAllTagNamesForUserQueryResultMessage(ActorState.SubscribedTags)));

            Receive<GetUserContentQueryMessage>(msg => Sender.Tell(new GetUserContentQueryResultMessage(((IEnumerable<UsersContent>)ActorState.Contents)
                .OrderByDescending(c => c.Added)
                .Skip((msg.CurrentPage - 1) * msg.Take)
                .Take(msg.Take))));

            Receive<GetUserContentDetailsQueryMessage>(msg => Sender.Tell(new GetUserContentDetailsQueryResultMessage(ActorState.Contents[msg.Uri])));
        }

        private void HandleAddContentCommand(AddContentCommand cmd)
        {
            var @event = new ContentAddedEvent(cmd.Uri, cmd.Title, cmd.ContentTags);
            ActorState.AddOrUpdateContent(@event.ContentUri, @event.Title, @event.ContentTags, @event.Added);
        }

        private void HandleRemoveUserContentCommand(RemoveUserContentCommand cmd)
        {
            var @event = new UserContentRemovedEvent(cmd.Uri);
            ActorState.HandleContentRemovedEvent(@event);
        }

        private void HandleRenameUserContentCommand(RenameUserContentCommand cmd)
        {
            var @event = new UserContentRenamedEvent(cmd.Uri, cmd.Title);
            ActorState.HandleContentRenamedEvent(@event);
        }

        private void HandleTagContentCommand(TagContentCommand cmd)
        {
            var @event = new ContentTaggedByUserEvent(cmd.Uri, cmd.ContentTags);
            ActorState.AddOrUpdateContentTags(@event.ContentUri, @event.ContentTags);
        }

        private void HandleAddSubscribedTagCommand(SubscribeToTagCommand cmd)
        {
            var @event = new TagSubscribedEvent(cmd.TagName);
            HandleTagSubscribedEvent(@event);
        }

        private bool HandleTagSubscribedEvent(TagSubscribedEvent ev)
        {
            return ActorState.AddSubscribedTag(ev.TagName);
        }

        private void HandleRateContentCommand(RateContentCommand cmd)
        {
            var @event = new ContentRatedEvent(cmd.Uri, cmd.Rating);
            ActorState.RateContent(@event.Uri, @event.Rating);
        }

        private async void HandleUserLoggedInMessageAsync(UserLoggedInCommand cmd)
        {

            using (var client = new GremlinCosmosClient())
            {
                //var ctSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
                //var existingVertexes = await client.Execute(GremlinQuery.g.V($"user-{cmd.UserName}"), vertex => vertex, ctSource.Token);
                //if (existingVertexes.IsNullOrEmpty())
                //    await client.Execute(GremlinQuery.g.addV("user", $"user-{cmd.UserName}").property("testInt", 1).property("testString", "string").property("testBool", true), vertex => vertex, new CancellationTokenSource(TimeSpan.FromSeconds(15)).Token);



            }
            ActorState.RefreshLastLoggedIn();
        }

        public static Props Props(string authenthicationType, string id)
        {
            return Akka.Actor.Props.Create<UserActor>(() => new UserActor(authenthicationType, id));
        }

        private sealed class UserActorState : UserDataBase
        {
            internal DateTime LastLoggedIn { get; private set; }
            private readonly HashSet<string> subscribedTags;
            internal IReadOnlyCollection<string> SubscribedTags => subscribedTags;

            //Refactor this. Nested command object/model with some kind of container?
            internal ContentContainer Contents { get; private set; }

            internal UserActorState(string authenthicationType, string id) : base(authenthicationType, id)
            {
                subscribedTags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                Contents = new ContentContainer();
                RefreshLastLoggedIn();
            }

            /// <returns>True if state has changed</returns>
            internal bool AddSubscribedTag(string tagName) => subscribedTags.Add(tagName);

            //TODO: DRY
            /// <returns>True if state has changed</returns>
            internal bool AddOrUpdateContent(Uri contentUri, string title, IEnumerable<string> contentTags = null, DateTime? added = null)
            {
                var result = false;
                if (!Contents.ContainsKey(contentUri))
                {
                    Contents.Add(new UsersContent(contentUri, title, contentTags, added ?? DateTime.UtcNow));
                    result = true;
                }
                else
                    result = Contents[contentUri].UpdateTags(contentTags);
                //TODO: Update title

                return result;
            }

            //TODO: DRY
            internal bool AddOrUpdateContentTags(Uri contentUri, IEnumerable<string> contentTags = null, DateTime? added = null)
            {
                var result = false;
                if (!Contents.ContainsKey(contentUri))
                {
                    Contents.Add(new UsersContent(contentUri, string.Empty, contentTags, added ?? DateTime.UtcNow));
                    result = true;
                }
                else
                    result = Contents[contentUri].UpdateTags(contentTags);

                return result;
            }
            internal void RefreshLastLoggedIn() => LastLoggedIn = DateTime.UtcNow;

            internal void RestoreLastLoggedIn(DateTime dateTime)
            {
                if (LastLoggedIn < dateTime)
                    LastLoggedIn = dateTime;
            }

            internal void RateContent(Uri uri, byte rating)
            {
                Contents[uri].Rate(rating);
            }

            internal bool HandleContentRemovedEvent(UserContentRemovedEvent ev) => Contents.Remove(ev.Uri);

            internal bool HandleContentRenamedEvent(UserContentRenamedEvent ev)
            {
                if (Contents.TryGetValue(ev.Uri, out UsersContent content))
                    return content.Rename(ev.Title);
                return false;
            }
        }
    }


}
