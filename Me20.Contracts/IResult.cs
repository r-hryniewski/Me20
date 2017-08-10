using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Contracts
{
    public interface IResult<TItem> : IResult
    {
        TItem Item { get; }
    }

    public interface IResult
    {
        bool Successful { get; }
        IEnumerable<string> ErrorList { get; }
    }
}
