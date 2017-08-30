using Akka.Actor;
using Akka.Event;
using MassTransit;
using Me20.Contracts;
using Me20.Shared.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.IdentityActors
{
    public class UserActor : ReceiveActor
    {
        private readonly ILoggingAdapter logger;
        private readonly ISendEndpointProvider sendEndpointProvider;
        private readonly IPublishEndpoint publishEndpoint;

        private readonly UserActorState state;

        public UserActor(IUserIdentity userIdentity, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
        {
            logger = Logging.GetLogger(Context);
            this.sendEndpointProvider = sendEndpointProvider;
            this.publishEndpoint = publishEndpoint;

            state = new UserActorState(userIdentity.Id, userIdentity.AuthenticationType);

            if (!state.TryRestore(sendEndpointProvider).GetAwaiter().GetResult())
                publishEndpoint.Publish(new { /*NewUserLoggedInEvent*/});
                
        }

        private class UserActorState : UserIdentityBase
        {
            public UserActorState(string id, string authenticationType) : base(id, authenticationType)
            {
            }

            internal async Task<bool> TryRestore(ISendEndpointProvider sendEndpointProvider)
            {
                //Request/Response to read service
                return false;
            }
        }
    }
}
