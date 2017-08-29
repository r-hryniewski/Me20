using Akka.Actor;
using Me20.ApiGateway.ViewModels;
using Me20.Contracts.Actors;
using Me20.IdentityActors;
using Nancy;

namespace Me20.ApiGateway.Modules
{
    public class LoginModule : NancyModule
    {
        //Will it be needed? If not - delete
        private readonly IActorRef userManagerActorRef;

        public LoginModule(IKnowActor<UsersManagerActor> userManagerActorRefContainer) : base("/login")
        {
            this.userManagerActorRef = userManagerActorRefContainer.Ref;
            Get["/"] = p =>
            {
                return View["login"];
            };
        }
    }
}
