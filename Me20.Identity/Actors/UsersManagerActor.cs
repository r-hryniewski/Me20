using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Common.Commands;
using Me20.Common.Interfaces;
using Me20.Identity.Commands;
using Me20.Identity.Interfaces;
using Me20.Identity.QueryMessages;
using System;

namespace Me20.Identity.Actors
{
    public class UsersManagerActor : ReceiveActorBase
    {
        private readonly IActorRef usersListActor;

        public UsersManagerActor() : base()
        {
            usersListActor = Context.ActorOf(UsersListActor.Props, Guid.NewGuid().ToString());

            Receive<UserLoggedInCommand>(msg => 
                {
                    usersListActor.Tell(msg.UserName);
                    HandleUserLoggedInMessage(msg);
                }, 
                msg => msg.IsValid);

            Receive<IHaveUserName>(msg => Context.Child(msg.UserName).Forward(msg));
        }

        private void HandleUserLoggedInMessage(UserLoggedInCommand msg)
        {
                var sendee = CreateUserActorIfNotExists(msg);
                sendee.Forward(msg);
        }

        private IActorRef CreateUserActorIfNotExists(IHaveAuthenthicationInfo authenthicationInfo)
        {
            var actorPath = authenthicationInfo.UserName;
            if (!Context.Child(actorPath).IsNobody())
                return Context.Child(actorPath);
            else
                return Context.ActorOf(UserActor.Props(authenthicationInfo.AuthenticationType, authenthicationInfo.Id), actorPath);
        }

        public static Props Props => Props.Create(() => new UsersManagerActor());
    }
}
