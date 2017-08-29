using Akka.Actor;
using Akka.Event;
using Me20.Contracts;
using System;
using MassTransit;
using Me20.Contracts.Events;

namespace Me20.IdentityActors
{
    public class UsersManagerActor : ReceiveActor
    {
        //private readonly IActorRef usersListActor;

        protected ILoggingAdapter Logger { get; private set; }

        public UsersManagerActor()
        {
            Logger = Logging.GetLogger(Context);
            //usersListActor = Context.ActorOf(UsersListActor.Props, Guid.NewGuid().ToString());

            Receive<IUserLoggedInEvent>(msg =>
            {
                //usersListActor.Tell(msg.UserName);
                HandleUserLoggedInMessage(msg);
            },
                msg => msg.IsValid);

            //Receive<IHaveUserName>(msg => Context.Child(msg.UserName).Forward(msg));
        }

        private void HandleUserLoggedInMessage(IUserLoggedInEvent msg)
        {
                var sendee = CreateUserActorIfNotExists(msg);
                sendee.Forward(msg);
        }

        private IActorRef CreateUserActorIfNotExists(IUserIdentity userIdentity)
        {
            var actorPath = userIdentity.UserName;
            if (!Context.Child(actorPath).IsNobody())
                return Context.Child(actorPath);
            else
                return null;// Context.ActorOf(UserActor.Props(userIdentity.AuthenticationType, userIdentity.Id), actorPath);
        }
    }
}
