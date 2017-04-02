using Akka.Actor;
using Akka.Persistence;
using Me20.Common.Abstracts;
using System;

namespace Me20.Content.Actors
{
    public class ContentActor : ReceivePersistentActorBase
    {
        private ContentActorState ActorState { get; set; }

        public override string PersistenceId => $"content-{ActorState.Uri}";

        public ContentActor(Uri uri) : base()
        {
            //validate url
            ActorState = new ContentActorState(uri);

            Recover<SnapshotOffer>(offer => ActorState = (ContentActorState)offer.Snapshot);
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
