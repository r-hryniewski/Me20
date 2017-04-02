using Akka.Actor;
using Akka.Persistence;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using Me20.Identity.Abstracts;
using Me20.Identity.Events;
using Me20.Identity.Commands;
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

            Command<UserLoggedInCommand>(cmd => HandleUserLoggedInMessage(cmd));
            Recover<UserLoggedInEvent>(ev => ActorState.RestoreLastLoggedIn(ev.LoginTime));

            Command<SubscribeToTagCommand>(cmd => HandleAddSubscribedTagCommand(cmd));
            Recover<TagSubscribedEvent>(ev => ActorState.AddSubscribedTag(ev.TagName));

            //TODO: Persistence
            Command<AddContentCommand>(msg => HandleAddContentCommand(msg));
            Recover<ContentAddedEvent>(ev => ActorState.AddContent(ev.ContentUri, ev.ContentTags));
        }

        private void HandleAddContentCommand(AddContentCommand cmd)
        {
            var @event = new ContentAddedEvent(cmd.ContentUri, cmd.ContentTags);
            if (ActorState.AddContent(@event.ContentUri, @event.ContentTags))
                Persist(@event, ev => HandleSnapshoting(ActorState));
        }

        private void HandleAddSubscribedTagCommand(SubscribeToTagCommand cmd)
        {
            var @event = new TagSubscribedEvent(cmd.TagName);
            if (ActorState.AddSubscribedTag(@event.TagName))
                Persist(@event, ev => HandleSnapshoting(ActorState));
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
            //NYI
            //internal IReadOnlyCollection<string> SubscribedTags => subscribedTags;

            internal Dictionary<string, HashSet<Uri>> ContentsByTags { get; private set; }
            internal HashSet<Uri> UntaggedContent { get; private set; }

            internal UserActorState(string authenthicationType, string id) : base(authenthicationType, id)
            {
                subscribedTags = new HashSet<string>();
                ContentsByTags = new Dictionary<string, HashSet<Uri>>(StringComparer.OrdinalIgnoreCase);
                UntaggedContent = new HashSet<Uri>(/*StringComparer.OrdinalIgnoreCase*/);
                RefreshLastLoggedIn();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="tagName"></param>
            /// <returns>True if state has changed</returns>
            internal bool AddSubscribedTag(string tagName) => subscribedTags.Add(tagName);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="contentUri"></param>
            /// <param name="contentTags"></param>
            /// <returns>True if state has changed</returns>
            internal bool AddContent(Uri contentUri, IEnumerable<string> contentTags = null)
            {
                if (contentTags == null || !contentTags.Any())
                    return UntaggedContent.Add(contentUri);

                else
                    return AddTaggedContent(contentUri, contentTags);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="contentUrl"></param>
            /// <param name="contentTags"></param>
            /// <returns>True if state has changed</returns>
            private bool AddTaggedContent(Uri contentUrl, IEnumerable<string> contentTags = null)
            {
                var result = false;
                foreach (var tag in contentTags)
                {
                    if (!ContentsByTags.ContainsKey(tag))
                        ContentsByTags.Add(tag, new HashSet<Uri>(/*StringComparer.OrdinalIgnoreCase*/));

                    if (ContentsByTags[tag].Add(contentUrl))
                        result = true;
                }
                return result;
            }

            internal void RefreshLastLoggedIn() => LastLoggedIn = DateTime.UtcNow;

            internal void RestoreLastLoggedIn(DateTime dateTime)
            {
                if (LastLoggedIn < dateTime)
                    LastLoggedIn = dateTime;
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
