using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Me20.Contracts
{
    public interface IHandleQueries<TQuery, TResult> : ICanValidate<TQuery> where TQuery : IQuery
    {
        Task<IQueryResult<TResult>> Handle(TQuery q, CancellationToken ct = default(CancellationToken));
    }
}
