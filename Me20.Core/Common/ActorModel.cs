using Akka.Actor;
using Me20.Core.Actors;

namespace Me20.Core
{
    public static class ActorModel
    {
        public static ActorSystem MainActorSystem { get; set; }

        public static IActorRef UsersManagerActorRef { get; set; }

        //TODO: ContentManager
        //TODO: Anonymous UserActor?

        public static void StartActorSystem()
        {
            //TODO: Actor names to some kind of constant values
            MainActorSystem = ActorSystem.Create("MainSystem");

            UsersManagerActorRef = MainActorSystem.ActorOf(UsersManagerActor.Props, "UsersManager");
        }
    }
}