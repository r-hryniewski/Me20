using Akka.Actor;
using Me20.Identity.Messages;

namespace Me20.Core.Actors
{
    public class UsersManagerActor : ReceiveActor
    {
        public UsersManagerActor()
        {
            Receive<UserLoggedInMessage>(msg => HandleUserLoggedInMessage(msg));
        }

        private void HandleUserLoggedInMessage(UserLoggedInMessage message)
        {
            if (!Context.Child(message.UserName).IsNobody())
                Context.Child(message.UserName).Tell(message);

            else
            {
                var newUserActor = Context.ActorOf(UserActor.Props, message.UserName);
                newUserActor.Tell(message);
            }
        }

        public static Props Props => Props.Create(() => new UsersManagerActor());
    }
}
