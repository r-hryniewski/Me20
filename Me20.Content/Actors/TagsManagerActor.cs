using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using Me20.Common.Messages;

namespace Me20.Content.Actors
{
    public class TagsManagerActor : ReceiveActorBase
    {
        public TagsManagerActor() : base()
        {
            Receive<CreateTagIfNotExistsMessage>(msg => HandleCreateTagIfNotExistsMessage(msg));
            Receive<AddSubscriberCommand>(msg => CreateTagActorIfNotExists(msg.TagName).Forward(msg));
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
                return Context.ActorOf(TagActor.Props(tagName), tagName);
        }

        public static Props Props => Props.Create(() => new TagsManagerActor());
    }
}
