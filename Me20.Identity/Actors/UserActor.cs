using Akka.Actor;
using Akka.Persistence;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
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

namespace Me20.Identity.Actors
{
    //TODO: Make it persistent actor
    public class UserActor : ReceivePersistentActorBase
    {
        private UserActorState ActorState { get; set; }

        public override string PersistenceId => $"user-{ActorState.UserName}";

        public UserActor(string authenthicationType, string id) : base()
        {
            ActorState = new UserActorState(authenthicationType, id);

            Recover<SnapshotOffer>(offer => ActorState = (UserActorState)offer.Snapshot);

            RegisterCommands();

            RegisterQueries();
        }

        //TODO: Some kind of collection that registers this stuff in foreach?
        private void RegisterCommands()
        {
            //Logging in persisted at 15 mins
            Command<UserLoggedInCommand>(cmd => HandleUserLoggedInMessage(cmd));
            Recover<UserLoggedInEvent>(ev => ActorState.RestoreLastLoggedIn(ev.LoginTime));
            //Tag subscribtion
            Command<SubscribeToTagCommand>(cmd => HandleAddSubscribedTagCommand(cmd));
            Recover<TagSubscribedEvent>(ev => HandleTagSubscribedEvent(ev));
            //Content added by user
            Command<AddContentCommand>(cmd => HandleAddContentCommand(cmd));
            Recover<ContentAddedEvent>(ev => ActorState.AddOrUpdateContent(ev.ContentUri, ev.Title, ev.ContentTags, ev.Added));
            //Content tagged by user
            Command<TagContentCommand>(cmd => HandleTagContentCommand(cmd));
            Recover<ContentTaggedByUserEvent>(ev => ActorState.AddOrUpdateContentTags(ev.ContentUri, ev.ContentTags));
            //Content rated by user
            Command<RateContentCommand>(cmd => HandleRateContentCommand(cmd));
            Recover<ContentRatedEvent>(ev => ActorState.RateContent(ev.Uri, ev.Rating));

        }

        private void RegisterQueries()
        {
            Command<GetAllTagNamesForUserQueryMessage>(msg => Sender.Tell(new GetAllTagNamesForUserQueryResultMessage(ActorState.SubscribedTags)));

            Command<GetUserContentQueryMessage>(msg => Sender.Tell(new GetUserContentQueryResultMessage(((IEnumerable<UsersContent>)ActorState.Contents)
                .OrderByDescending(c => c.Added)
                .Skip((msg.CurrentPage - 1) * msg.Take)
                .Take(msg.Take))));

            Command<GetUserContentDetailsQueryMessage>(msg => Sender.Tell(new GetUserContentDetailsQueryResultMessage(ActorState.Contents[msg.Uri])));
        }
        private void HandleAddContentCommand(AddContentCommand cmd)
        {
            var @event = new ContentAddedEvent(cmd.Uri, cmd.Title, cmd.ContentTags);
            if (ActorState.AddOrUpdateContent(@event.ContentUri, @event.Title, @event.ContentTags, @event.Added))
                Persist(@event, ev => HandleSnapshoting(ActorState));
        }

        private void HandleTagContentCommand(TagContentCommand cmd)
        {
            var @event = new ContentTaggedByUserEvent(cmd.Uri, cmd.ContentTags);
            if (ActorState.AddOrUpdateContentTags(@event.ContentUri, @event.ContentTags))
                Persist(@event, ev => HandleSnapshoting(ActorState));
        }

        private void HandleAddSubscribedTagCommand(SubscribeToTagCommand cmd)
        {
            var @event = new TagSubscribedEvent(cmd.TagName);
            if (HandleTagSubscribedEvent(@event))
                Persist(@event, ev => HandleSnapshoting(ActorState));
        }

        private bool HandleTagSubscribedEvent(TagSubscribedEvent ev)
        {
            return ActorState.AddSubscribedTag(ev.TagName);
        }

        private void HandleRateContentCommand(RateContentCommand cmd)
        {
            var @event = new ContentRatedEvent(cmd.Uri, cmd.Rating);
            Persist(@event, ev =>
            {
                ActorState.RateContent(ev.Uri, ev.Rating);
                HandleSnapshoting(ActorState);
            }
            );
        }

        private void HandleUserLoggedInMessage(UserLoggedInCommand cmd)
        {
            if ((DateTime.UtcNow - ActorState.LastLoggedIn).TotalMinutes > 15)
            {
                var @event = new UserLoggedInEvent(DateTime.UtcNow);
                Persist(@event, ev => HandleSnapshoting(ActorState));
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

            //TODO: Refactor this
            //Not used at the moment
            //internal bool HasChanged(UserLoggedInMessage msg)
            //{
            //    return (new bool[]
            //    {
            //        this.UserName.Equals(msg.UserName, StringComparison.OrdinalIgnoreCase),
            //        //this.Id.Equals(msg.Id, StringComparison.OrdinalIgnoreCase),
            //        //this.FullName.Equals(msg.FullName, StringComparison.OrdinalIgnoreCase),
            //        //this.FirstName.Equals(msg.FirstName, StringComparison.OrdinalIgnoreCase),
            //        //this.LastName.Equals(msg.LastName, StringComparison.OrdinalIgnoreCase),
            //        //this.Email.Equals(msg.Email, StringComparison.OrdinalIgnoreCase),
            //        //this.Gender.Equals(msg.Gender, StringComparison.OrdinalIgnoreCase),
            //        //this.AuthenticationType.Equals(msg.AuthenticationType, StringComparison.OrdinalIgnoreCase)
            //    })
            //    .Any(x => !x);
            //}

            //TODO: Change type
            //Not used at the moment
            //internal void Update(UserLoggedInMessage msg)
            //{
            //    //TODO: Not used atm

            //    //this.Id = msg.Id;
            //    //this.FullName = msg.FullName;
            //    //this.FirstName = msg.FirstName;
            //    //this.LastName = msg.LastName;
            //    //this.Email = msg.Email;
            //    //this.Gender = msg.Gender;
            //    //this.AuthenticationType = msg.AuthenticationType;
            //}
        }
    }
}
