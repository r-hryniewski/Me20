using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Contracts
{
    public interface ICommandResult : IResult
    {
        ICommandResult AddError(string errorMsg);
        ICommandResult AddUnexpectedError();
        ICommandResult AddErrorsFrom(IResult otherResult);
    }
}
