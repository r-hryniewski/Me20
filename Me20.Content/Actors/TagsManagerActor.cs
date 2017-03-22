using Akka.Actor;
using Me20.Common.Messages;

namespace Me20.Content.Actors
{
    public class TagsManagerActor : ReceiveActor
    {
        public TagsManagerActor()
        {
            Receive<CreateTagIfNotExistsMessage>(msg => HandleCreateTagIfNotExistsMessage(msg));
        }

        private void HandleCreateTagIfNotExistsMessage(CreateTagIfNotExistsMessage msg)
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
