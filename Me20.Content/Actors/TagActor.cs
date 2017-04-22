using Akka.Actor;
using Akka.Persistence;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using Me20.Common.Comparers;
using Me20.Common.Interfaces;
using Me20.Content.Events.Tags;
using System;
using System.Collections.Generic;

namespace Me20.Content.Actors
{
    public class TagActor : ReceivePersistentActorBase
    {
        private TagActorState ActorState { get; set; }

        public override string PersistenceId => $"tag-{ActorState.TagName}";

        public TagActor(string tagName) : base()
        {
            ActorState = new TagActorState(tagName);

            Recover<SnapshotOffer>(offer => ActorState = (TagActorState)offer.Snapshot);

            Command<SubscribeToTagCommand>(cmd => HandleAddSubscriberCommand(cmd));
            Recover<SubscriberAddedEvent>(ev => ActorState.AddSubscriber(ev));

            Command<AddContentCommand>(cmd => HandleAddContentCommand(cmd));
            Recover<ContentTaggedEvent>(ev => ActorState.AddContent(ev.Uri));
        }

        private void HandleAddContentCommand(AddContentCommand cmd)
        {
            var @event = new ContentTaggedEvent(cmd.Uri);
            if (ActorState.AddContent(@event.Uri))
                Persist(@event, ev => HandleSnapshoting(ActorState));
        }

        private void HandleAddSubscriberCommand(SubscribeToTagCommand cmd)
        {
            var @event = new SubscriberAddedEvent(cmd.UserName);

            //Persist only if subscribber is actually added to state
            if (ActorState.AddSubscriber(@event))
                Persist(@event, ev => HandleSnapshoting(ActorState));
        }

        public static Props Props(string tagName) => Akka.Actor.Props.Create(() => new TagActor(tagName));

        private sealed class TagActorState
        {
            internal string TagName { get; private set; }
            private readonly HashSet<string> subscribers;
            //NYI
            //internal IReadOnlyCollection<string> Subscribers => subscribers;

            private readonly HashSet<Uri> contents;
            internal IReadOnlyCollection<Uri> Contents => contents;

            internal TagActorState(string tagName)
            {
                TagName = tagName;
                subscribers = new HashSet<string>();
                contents = new HashSet<Uri>(new SchemalessBase64UriComparer());
            }

            internal bool AddSubscriber(IHaveUserName userNameContainer) => subscribers.Add(userNameContainer.UserName);

            internal bool AddContent(Uri uri) => contents.Add(uri);
        }
    }
}
