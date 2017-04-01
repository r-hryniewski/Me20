using Akka.Actor;
using Akka.Persistence;
using Me20.Common.Commands;
using System.Collections.Generic;
using Me20.Common.Interfaces;

namespace Me20.Content.Actors
{
    public class TagActor : ReceivePersistentActor
    {
        private TagActorState ActorState { get; set; }

        public override string PersistenceId => $"tag-{ActorState.TagName}";

        //move to base class along with Snapshot saving and command counter incrementation
        private ushort x = 0;
        private readonly ushort snapshotInterval = 100;

        public TagActor(string tagName) : base()
        {
            ActorState = new TagActorState(tagName);

            Command<AddSubscriberCommand>(msg => HandleAddSubscriberCommand(msg));
            Recover<SubscriberAddedEvent>(sae => ActorState.AddSubscriber(sae.UserName));
            //TODO: Create container for content marked with this tag
            //TODO: Handle receiving added tagged content message

            Recover<SnapshotOffer>(offer =>
            {
                ActorState = (TagActorState)offer.Snapshot;
            });
        }

        private void HandleAddSubscriberCommand(AddSubscriberCommand msg)
        {
            var @event = new SubscriberAddedEvent(msg.UserName);
            //TODO: Persist only if subscribber is actually added to state
            Persist(@event, ev =>
            {
                ActorState.AddSubscriber(ev.UserName);
                x++;
                if (x >= snapshotInterval)
                {
                    SaveSnapshot(ActorState);
                    x = 0;
                }

            });
        }

        public static Props Props(string tagName) => Akka.Actor.Props.Create(() => new TagActor(tagName));

        private sealed class TagActorState
        {
            internal string TagName { get; private set; }
            private readonly HashSet<string> subscribers;

            //NYI
            //internal IReadOnlyCollection<string> Subscribers => subscribers;

            internal TagActorState(string tagName)
            {
                TagName = tagName;
                subscribers = new HashSet<string>();
            }

            internal void AddSubscriber(string subscriberUserName)
            {
                subscribers.Add(subscriberUserName);
            }
        }

        //TODO: Move event somewhere else when class is finished
        public class SubscriberAddedEvent : IHaveUserName
        {
            public string UserName { get; private set; }

            public SubscriberAddedEvent(string userName)
            {
                UserName = userName;
            }
        }
    }
}
