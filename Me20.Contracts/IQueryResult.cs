using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Contracts
{
    public interface IQueryResult<TItem> : IResult<TItem>
    {
        IQueryResult<TItem> AddError(string errorMsg);
        IQueryResult<TItem> AddUnexpectedError();
        IQueryResult<TItem> AddErrorsFrom(IResult otherResult);
    }
}
