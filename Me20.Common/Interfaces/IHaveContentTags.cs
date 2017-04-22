using System.Collections.Generic;

namespace Me20.Common.Interfaces
{
    public interface IHaveContentTags
    {
        IEnumerable<string> ContentTags { get; }
    }
}
