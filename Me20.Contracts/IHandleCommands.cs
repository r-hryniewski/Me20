using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Me20.Contracts
{
    public interface IHandleCommands<TCommand> : ICanValidate<TCommand> where TCommand : ICommand
    {
        Task<ICommandResult> Handle(TCommand cmd, CancellationToken ct = default(CancellationToken));
    }
}
