using Me20.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Me20.Content.QueryResultMessages
{
    public class GetTagsListQueryResultMessage
    {
        public IReadOnlyCollection<string> TagsList {get; private set;}

        public GetTagsListQueryResultMessage(IEnumerable<string> tagsList)
        {
            TagsList = new List<string>(tagsList);
        }
    }
}
