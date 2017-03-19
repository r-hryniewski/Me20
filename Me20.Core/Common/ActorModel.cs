using Akka.Actor;
using Me20.Content.Actors;
using Me20.Identity.Actors;
using static Me20.Common.Helpers.ActorPathsHelper;

namespace Me20.Core
{
    public static class ActorModel
    {
        public static ActorSystem MainActorSystem { get; set; }

        public static IActorRef UsersManagerActorRef { get; set; }
        public static IActorRef TagsManagerActorRef { get; set; }

        //TODO: ContentManager
        //TODO: Anonymous UserActor?

        public static void StartActorSystem()
        {
            MainActorSystem = ActorSystem.Create(ActorSystemName);

            UsersManagerActorRef = MainActorSystem.ActorOf(UsersManagerActor.Props, UsersManagerActorName);
            TagsManagerActorRef = MainActorSystem.ActorOf(TagsManagerActor.Props, TagsManagerActorName);
        }

        public static ActorSelection GetUserActorSelection(string userName) => MainActorSystem.ActorSelection($"{UsersManagerActorRef.Path.ToStringWithAddress()}/{userName}");
    }
}