using Akka.Actor;
using Me20.Common.Abstracts;

namespace Me20.Content.Actors
{
    public class ContentActor : ReceiveActorBase
    {
        private ContentActorState ActorState { get; set; }

        public ContentActor(string url) : base()
        {
            //validate url
            ActorState = new ContentActorState(url);
            
            //TODO: Handle receiving added tagged content message
        }

        public static Props Props(string url) => Akka.Actor.Props.Create(() => new ContentActor(url));

        private sealed class ContentActorState
        {
            internal string Url { get; private set; }
            
            internal ContentActorState(string url)
            {
                Url = url;
                
                //TODO: Rating
                //TODO: Level
                //TODO: ReadedBy
                //TODO: TimeLastOpened
            }
        }
    }
}
