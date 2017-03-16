using Akka.Actor;
using Me20.Identity.Messages;
using Me20.Identity.Actors;
using Me20.Common.Messages;
using Me20.Common.Commands;

namespace Me20.Core.Actors
{
    public class UsersManagerActor : ReceiveActor
    {
        public UsersManagerActor()
        {
            Receive<UserLoggedInMessage>(msg => HandleUserLoggedInMessage(msg));

            Receive<TagSubscribedMessage>(msg => HandleTagSubscribedMessage(msg));
        }

        private void HandleTagSubscribedMessage(TagSubscribedMessage msg)
        {
            var sendee = CreateUserActorIfNotExists(msg.ByUserName);

            sendee.Tell(new AddSubscribedTagCommand(msg.TagName));
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
