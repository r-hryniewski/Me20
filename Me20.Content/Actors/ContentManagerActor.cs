using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Extensions;
using Me20.Common.Messages;
using System;

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
            CreateContentActorIfNotExists(msg.Uri);
        }

        private IActorRef CreateContentActorIfNotExists(Uri uri)
        {
            var actorPathSegment = uri.ToActorPathSegment();
            if (!Context.Child(actorPathSegment).IsNobody())
                return Context.Child(actorPathSegment);

            else
                return Context.ActorOf(ContentActor.Props(uri), actorPathSegment);
        }

        public static Props Props => Props.Create(() => new ContentManagerActor());
    }
}
