using System;
using System.Collections.Generic;

namespace Me20.Identity.QueryResultMessages
{
    public class GetUserContentQueryResultMessage
    {
        public IReadOnlyDictionary<Uri, HashSet<string>> ContentWithTags { get; private set; }

        public GetUserContentQueryResultMessage(IReadOnlyDictionary<Uri, HashSet<string>> contentsWithTags)
        {
            ContentWithTags = contentsWithTags;
        }
    }
}
