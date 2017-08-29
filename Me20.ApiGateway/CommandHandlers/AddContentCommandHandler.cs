using Me20.Contracts.Commands;
using Me20.Shared.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Me20.Contracts;
using Me20.Shared.Results;
using System.Threading.Tasks;
using Me20.ApiGateway.Commands;
using System.Threading;
using Me20.Contracts.Actors;
using Me20.IdentityActors;
using MassTransit;
using Akka.Actor;

namespace Me20.ApiGateway.CommandHandlers
{
    public class AddContentCommandHandler : CommandHandlerBase<Commands.AddContent>
    {
        private readonly IKnowActor<UsersManagerActor> usersManagerActorContainer;
        private readonly IUserIdentity userIdentity;
        private readonly ISendEndpointProvider endpointProvider;

        public AddContentCommandHandler(IKnowActor<UsersManagerActor> usersManagerActorContainer, IUserIdentity userIdentity, ISendEndpointProvider endpointProvider)
        {
            this.usersManagerActorContainer = usersManagerActorContainer;
            this.userIdentity = userIdentity;
            this.endpointProvider = endpointProvider;
        }

        public override IEnumerable<Action<Commands.AddContent, ValidationResult>> Validators { get
            {
                yield return (cmd, result) =>
                {
                    if (cmd.ContentUri == null)
                        result.AddError($"Posted Url ({cmd.Url}) is invalid");
                };
            }
        }

        protected override async Task<ICommandResult> ExecuteCommand(AddContent command, ICommandResult result, CancellationToken ct = default(CancellationToken))
        {
            if (userIdentity != null && userIdentity.IsValid)
                SendCommandToMyUserActor(command);
            else
                await SendCommandToContentService(command, ct);

            return result;
        }

        private async Task SendCommandToContentService(AddContent command, CancellationToken ct)
        {
            var endpoint = await endpointProvider.GetSendEndpoint(Shared.BusConfig.ContentWriteQueueUri);
            await endpoint.Send<IAddContentCommand>(command, ct);
        }

        private void SendCommandToMyUserActor(AddContent command)
        {
            usersManagerActorContainer.Ref.Tell(new AddMyContentCommand(command.ContentUri, userIdentity.UserName, command.Tags));
        }
    }

    public class AddMyContentCommand : IAddContentCommand, IHaveUserName
    {
        public Uri ContentUri { get; private set; }
        public string UserName { get; private set; }
        public IEnumerable<string> Tags { get; private set; }

        public AddMyContentCommand(Uri contentUri, string userName, IEnumerable<string> tags)
        {
            ContentUri = contentUri;
            UserName = userName;
            Tags = tags ?? Enumerable.Empty<string>();
        }

    }
}