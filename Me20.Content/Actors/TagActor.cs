using Akka.Actor;
using Me20.Common.Commands;
using System.Collections.Generic;

namespace Me20.Content.Actors
{
    public class TagActor : ReceiveActor
    {
        private TagActorState ActorState { get; set; }

        public TagActor(string tagName)
        {
            ActorState = new TagActorState(tagName);

            Receive<AddSubscriberCommand>(msg => HandleAddSubscriberCommand(msg));
            //TODO: Create container for content marked with this tag
            //TODO: Handle receiving added tagged content message
        }

        private void HandleAddSubscriberCommand(AddSubscriberCommand msg)
        {
            ActorState.AddSubscriber(msg.UserName);
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
    }
}
