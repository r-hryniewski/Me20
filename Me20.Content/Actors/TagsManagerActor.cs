using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using Me20.Common.Interfaces;

namespace Me20.Content.Actors
{
    public class TagsManagerActor : ReceiveActorBase
    {
        public TagsManagerActor() : base()
        {
            Receive<SubscribeToTagCommand>(msg => CreateTagActorIfNotExists(msg.TagName).Forward(msg));

            Receive<IHaveContentTag>(msg => CreateTagActorIfNotExists(msg.ContentTag).Forward(msg));

            Receive<IHaveContentTags>(msg =>
            {
                foreach (var tag in msg.ContentTags)
                    CreateTagActorIfNotExists(tag).Forward(msg);
            });
        }

        private IActorRef CreateTagActorIfNotExists(string tagName)
        {
            var actorPath = ChildActorPathValidator(tagName);
            if (!Context.Child(actorPath).IsNobody())
                return Context.Child(actorPath);

            else
                return Context.ActorOf(TagActor.Props(tagName), actorPath);
        }

        public static Props Props => Props.Create(() => new TagsManagerActor());
    }
}
