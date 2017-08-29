using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Contracts
{
    public interface IHaveTags
    {
        IEnumerable<string> Tags { get; }
    }
}
