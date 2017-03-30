using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Messages;

namespace Me20.Content.Actors
{
    public class ContentManagerActor : ReceiveActorBase
    {
        public ContentManagerActor() : base()
        {
            Receive<CreateContentIfNotExistsMessage>(msg => HandleCreateContentIfNotExistsMessage(msg));
        }

        private void HandleCreateContentIfNotExistsMessage(CreateContentIfNotExistsMessage msg)
        {
            CreateContentActorIfNotExists(msg.Url);
        }

        private IActorRef CreateContentActorIfNotExists(string url)
        {
            if (!Context.Child(url).IsNobody())
                return Context.Child(url);

            else
                return Context.ActorOf(ContentActor.Props(url), url);
        }

        public static Props Props => Props.Create(() => new ContentManagerActor());
    }
}
