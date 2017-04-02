using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;

namespace Me20.Content.Actors
{
    public class TagsManagerActor : ReceiveActorBase
    {
        public TagsManagerActor() : base()
        {
            Receive<SubscribeToTagCommand>(msg => CreateTagActorIfNotExists(msg.TagName).Forward(msg));
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
