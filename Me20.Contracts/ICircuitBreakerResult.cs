using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Contracts
{
    public interface ICircuitBreakerResult<TItem> : ICircuitBreakerResult, IResult<TItem>
    {
        //marker
    }

    public interface ICircuitBreakerResult : IResult
    {
        bool Break { get; }
        ICircuitBreakerResult AddError(string errorMsg, bool breakAfterAddingError = false);
        ICircuitBreakerResult AddUnexpectedError(bool breakAfterAddingError = false);
        ICircuitBreakerResult AddErrorsFrom(IResult otherResult);
    }
}
