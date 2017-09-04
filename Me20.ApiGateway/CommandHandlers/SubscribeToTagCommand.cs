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
    public class SubscribeToTagCommandHandler : CommandHandlerBase<Commands.SubscribeToTagCommand>
    {
        private readonly IKnowActor<UsersManagerActor> usersManagerActorContainer;
        private readonly IUserIdentity userIdentity;

        public SubscribeToTagCommandHandler(IKnowActor<UsersManagerActor> usersManagerActorContainer, IUserIdentity userIdentity)
        {
            this.usersManagerActorContainer = usersManagerActorContainer;
            this.userIdentity = userIdentity;
        }

        public override IEnumerable<Action<Commands.SubscribeToTagCommand, ValidationResult>> Validators
        {
            get
            {
                yield return (cmd, result) =>
                {
                    if (userIdentity == null || !userIdentity.IsValid)
                        result.AddError($"User is not authenticated or is invalid", true);
                };
                yield return (cmd, result) =>
                {
                    if (string.IsNullOrWhiteSpace(cmd.TagName))
                        result.AddError($"Parameter {nameof(cmd.TagName)} cannot be empty");
                };
                //TODO: Tag length
            }
        }

        protected override async Task<ICommandResult> ExecuteCommand(Commands.SubscribeToTagCommand command, ICommandResult result, CancellationToken ct = default(CancellationToken))
        {
            SendCommandToMyUserActor(command);

            return result;
        }

        private void SendCommandToMyUserActor(Commands.SubscribeToTagCommand command)
        {
            usersManagerActorContainer.Ref.Tell(new SubscribeToTagCommand(command.TagName, userIdentity.UserName));
        }

        private class SubscribeToTagCommand : ISubscribeToTagCommand
        {
            public string TagName { get; private set; }
            public string UserName { get; private set; }

            public SubscribeToTagCommand(string tagName, string userName)
            {
                TagName = tagName;
                UserName = userName;
            }
        }
    }
}