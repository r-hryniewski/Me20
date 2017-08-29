using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using Me20.Common.Extensions;
using Me20.Common.Interfaces;
using System;

namespace Me20.Content.Actors
{
    public class ContentManagerActor : ReceiveActorBase
    {
        private readonly IActorRef contentListActor;

        public ContentManagerActor() : base()
        {
            contentListActor = Context.ActorOf(ContentListActor.Props, Guid.NewGuid().ToString());

            Receive<AddContentCommand>(msg =>
            {
                contentListActor.Tell(msg.Uri);
                CreateContentActorIfNotExists(msg.Uri).Forward(msg);
            });
            Receive<IHaveContentUri>(msg => CreateContentActorIfNotExists(msg.Uri).Forward(msg));
        }

        private IActorRef CreateContentActorIfNotExists(Uri uri)
        {
            var actorPathSegment = uri.ToSchemalessUriAsMD5();
            if (!Context.Child(actorPathSegment).IsNobody())
                return Context.Child(actorPathSegment);

            else
                return Context.ActorOf(ContentActor.Props(uri), actorPathSegment);
        }

        public static Props Props => Props.Create(() => new ContentManagerActor());
    }
}
