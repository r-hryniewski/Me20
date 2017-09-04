using Akka.Actor;
using Akka.Event;
using MassTransit;
using Me20.Contracts;
using Me20.Contracts.Commands;
using Me20.Contracts.Entities;
using Me20.IdentityActors.Commands;
using Me20.IdentityActors.ValueObjects;
using Me20.Shared.Abstracts;
using Me20.Shared.Utilities;
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

        private readonly UserActorState state;

        public UserActor(IUserIdentity userIdentity, ISendEndpointProvider sendEndpointProvider)
        {
            logger = Logging.GetLogger(Context);
            this.sendEndpointProvider = sendEndpointProvider;

            state = new UserActorState(userIdentity.Id, userIdentity.AuthenticationType);

            if (!state.TryRestore(sendEndpointProvider).GetAwaiter().GetResult())
                SendCreateNewUserCommandAsync();

            ReceiveAsync<IAddMyContentCommand>(async (cmd) => await HandleAddContentCommand(cmd));
            ReceiveAsync<ISubscribeToTagCommand>(async (cmd) => await SubscribeToTagCommand(cmd));
        }

        private async Task SubscribeToTagCommand(ISubscribeToTagCommand cmd)
        {
            var endpoint = await sendEndpointProvider.GetSendEndpoint(Shared.BusConfig.ContentWriteQueueUri);
            await endpoint.Send(cmd);
            state.AddTag(cmd.TagName);
        }

        private async Task HandleAddContentCommand(IAddMyContentCommand cmd)
        {
            var enpoint = await sendEndpointProvider.GetSendEndpoint(Shared.BusConfig.ContentWriteQueueUri);
            await enpoint.Send(cmd);
            state.AddContent(new UsersContent(cmd.ContentUri, cmd.Tags));
        }

        private async Task SendCreateNewUserCommandAsync()
        {
            var endpoint = await sendEndpointProvider.GetSendEndpoint(Shared.BusConfig.IdentityWriteQueueUri);
            await endpoint.Send<ICreateNewUserCommand>(new CreateNewUserCommand(
                id: state.Id,
                authenticationType: state.AuthenticationType));
        }


        private class UserActorState : UserIdentityBase
        {
            ICollection<IContent> Contents { get; set; }
            ICollection<string> SubscribedTags { get; set; }

            public UserActorState(string id, string authenticationType) : base(id, authenticationType)
            {
                Contents = new HashSet<IContent>(EntityComparer.Instance);
                SubscribedTags = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            }

            internal async Task<bool> TryRestore(ISendEndpointProvider sendEndpointProvider)
            {
                //Request/Response to read service
                return false;
            }

            internal void AddContent(IContent content)
            {
                Contents.Add(content);
            }

            internal void AddTag(string tagName)
            {
                SubscribedTags.Add(tagName);
            }
        }
    }
}
