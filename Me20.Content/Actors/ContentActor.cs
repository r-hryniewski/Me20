using Akka.Actor;
using Me20.Common.Abstracts;
using System;

namespace Me20.Content.Actors
{
    public class ContentActor : ReceiveActorBase
    {
        private ContentActorState ActorState { get; set; }

        public ContentActor(Uri uri) : base()
        {
            //validate url
            ActorState = new ContentActorState(uri);
            
            //TODO: Handle receiving added tagged content message
        }

        public static Props Props(Uri uri) => Akka.Actor.Props.Create(() => new ContentActor(uri));

        private sealed class ContentActorState
        {
            internal Uri Uri { get; private set; }
            
            internal ContentActorState(Uri uri)
            {
                Uri = uri;
                
                //TODO: Rating
                //TODO: Level
                //TODO: ReadedBy
                //TODO: TimeLastOpened
            }
        }
    }
}
