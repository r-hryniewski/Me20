using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Identity.Interfaces;
using Me20.Identity.Messages;

namespace Me20.Identity.Actors
{
    public class UsersManagerActor : ReceiveActorBase
    {
        public UsersManagerActor() : base()
        {
            Receive<UserLoggedInMessage>(msg => HandleUserLoggedInMessage(msg));
        }

        private void HandleUserLoggedInMessage(UserLoggedInMessage msg)
        {
            if (msg.IsValid)
            {
                var sendee = CreateUserActorIfNotExists(msg);
                sendee.Tell(msg);
            }
        }

        private IActorRef CreateUserActorIfNotExists(IHaveAuthenthicationInfo authenthicationInfo)
        {
            if (!Context.Child(authenthicationInfo.UserName).IsNobody())
                return Context.Child(authenthicationInfo.UserName);
            else
                return Context.ActorOf(UserActor.Props(authenthicationInfo.AuthenticationType, authenthicationInfo.Id), authenthicationInfo.UserName);
        }

        public static Props Props => Props.Create(() => new UsersManagerActor());
    }
}
