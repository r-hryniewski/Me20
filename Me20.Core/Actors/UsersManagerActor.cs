using Akka.Actor;
using Me20.Identity.Messages;
using Me20.Indentity.Actors;

namespace Me20.Core.Actors
{
    public class UsersManagerActor : ReceiveActor
    {
        public UsersManagerActor()
        {
            Receive<UserLoggedInMessage>(msg => HandleUserLoggedInMessage(msg));
        }

        private void HandleUserLoggedInMessage(UserLoggedInMessage msg)
        {
            if (!Context.Child(msg.UserName).IsNobody())
                Context.Child(msg.UserName).Tell(msg);

            else
            {
                var newUserActor = Context.ActorOf(UserActor.Props, msg.UserName);
                newUserActor.Tell(msg);
            }
        }

        public static Props Props => Props.Create(() => new UsersManagerActor());
    }
}
