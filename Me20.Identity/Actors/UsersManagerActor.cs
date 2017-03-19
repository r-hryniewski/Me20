using Akka.Actor;
using Me20.Identity.Messages;

namespace Me20.Identity.Actors
{
    public class UsersManagerActor : ReceiveActor
    {
        public UsersManagerActor()
        {
            Receive<UserLoggedInMessage>(msg => HandleUserLoggedInMessage(msg));
        }

        private void HandleUserLoggedInMessage(UserLoggedInMessage msg)
        {
            var sendee = CreateUserActorIfNotExists(msg.UserName);
            sendee.Tell(msg);
        }

        private IActorRef CreateUserActorIfNotExists(string userName)
        {
            if (!Context.Child(userName).IsNobody())
                return Context.Child(userName);
            else
                return Context.ActorOf(UserActor.Props, userName);
        }

        public static Props Props => Props.Create(() => new UsersManagerActor());
    }
}
