using Akka.Actor;
using Me20.Common.Commands;
using Me20.Common.Helpers;
using Me20.Common.Messages;
using Me20.Content.Actors;

namespace Me20.Core.Actors
{
    public class TagManagerActor : ReceiveActor
    {
        public TagManagerActor()
        {
            Receive<TagSubscribedMessage>(msg => HandleTagAddedMessage(msg));
        }

        private void HandleTagAddedMessage(TagSubscribedMessage msg)
        {
            var sendee = CreateTagActorIfNotExists(msg.TagName);

            sendee.Tell(new AddSubscriberCommand(msg.ByUserName));
        }

        private IActorRef CreateTagActorIfNotExists(string tagName)
        {
            if (!Context.Child(tagName).IsNobody())
                return Context.Child(tagName);

            else
                return Context.ActorOf(TagActor.Props, tagName);
        }

        public static Props Props => Props.Create(() => new TagManagerActor());
    }
}
