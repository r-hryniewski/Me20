using Akka.Actor;

namespace Me20.Core.Helpers
{
    public static class ActorModelHelper
    {
        public static ActorSelection GetUserActorSelection(string userName) => ActorModel.MainActorSystem.ActorSelection($"{ActorModel.UsersManagerActorRef.Path.ToStringWithAddress()}/{userName}");
    }
}
