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

namespace Me20.ApiGateway.CommandHandlers
{
    public class AddContentCommandHandler : CommandHandlerBase<Commands.AddContent>
    {
        IKnowActor<UsersManagerActor> usersManagerActorContainer;
        IUserIdentity userIdentity;

        public AddContentCommandHandler(IKnowActor<UsersManagerActor> usersManagerActorContainer, IUserIdentity userIdentity)
        {
            this.usersManagerActorContainer = usersManagerActorContainer;
            this.userIdentity = userIdentity;
        }

        public override IEnumerable<Action<Commands.AddContent, ValidationResult>> Validators { get
            {
                yield return (cmd, result) =>
                {
                    if (cmd.ContentUri == null)
                        result.AddError($"Posted Uri ({cmd}) is invalid");
                };
            }
        }

        protected override Task<CommandResult> ExecuteCommand(AddContent command, ICommandResult result, CancellationToken ct = default(CancellationToken))
        {
            
        }
    }
}