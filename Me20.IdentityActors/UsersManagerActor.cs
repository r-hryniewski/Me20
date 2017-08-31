using Akka.Actor;
using Akka.Event;
using Me20.Contracts;
using System;
using MassTransit;
using Me20.Contracts.Events;
using Akka.DI.Core;

namespace Me20.IdentityActors
{
    public class UsersManagerActor : ReceiveActor
    {
        //private readonly IActorRef usersListActor;

        private ILoggingAdapter Logger { get; set; }

        private readonly ISendEndpointProvider sendEndpointProvider;

        public UsersManagerActor(ISendEndpointProvider sendEndpointProvider)
        {
            Logger = Logging.GetLogger(Context);
            this.sendEndpointProvider = sendEndpointProvider;
            //usersListActor = Context.ActorOf(UsersListActor.Props, Guid.NewGuid().ToString());

            Receive<IUserLoggedInEvent>(msg =>
            {
                //usersListActor.Tell(msg.UserName);
                HandleUserLoggedInMessage(msg);
            },
                msg => msg.IsValid);

            Receive<IHaveUserName>(msg => Context.Child(msg.UserName).Forward(msg));
        }

        private void HandleUserLoggedInMessage(IUserLoggedInEvent msg)
        {
            /*var sendee = */
            CreateUserActorIfNotExists(msg);
            //sendee.Forward(msg);
        }

        private IActorRef CreateUserActorIfNotExists(IUserIdentity userIdentity)
        {

            var actorPath = userIdentity.UserName;
            if (!Context.Child(actorPath).IsNobody())
                return Context.Child(actorPath);
            else
            {
                return Context.ActorOf(Props.Create<UserActor>(() => new UserActor(userIdentity, sendEndpointProvider)), actorPath);
            }
        }
    }
}
