using Akka.Actor;
using Me20.Common.Messages;
using Me20.Content.Actors;

namespace Me20.Core.Actors
{
    public class TagsManagerActor : ReceiveActor
    {
        public TagsManagerActor()
        {
            Receive<CreateTagIfNotExistsMessage>(msg => HandleTagAddedMessage(msg));
        }

        private void HandleTagAddedMessage(CreateTagIfNotExistsMessage msg)
        {
            CreateTagActorIfNotExists(msg.TagName);
        }

        private IActorRef CreateTagActorIfNotExists(string tagName)
        {
            if (!Context.Child(tagName).IsNobody())
                return Context.Child(tagName);

            else
                return Context.ActorOf(TagActor.Props, tagName);
        }

        public static Props Props => Props.Create(() => new TagsManagerActor());
    }
}
