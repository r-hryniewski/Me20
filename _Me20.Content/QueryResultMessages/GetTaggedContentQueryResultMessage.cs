using System;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Content.QueryResultMessages
{
    public class GetTaggedContentQueryResultMessage
    {
        public IEnumerable<Uri> Contents { get; private set; }
        public GetTaggedContentQueryResultMessage(IEnumerable<Uri> contents)
        {
            Contents = contents;
        }
    }
}
