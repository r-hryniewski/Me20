using Me20.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Identity.QueryResultMessages
{
    public class GetUserContentQueryResultMessage
    {
        public IReadOnlyDictionary<Uri, HashSet<string>> ContentWithTags { get; private set; }

        internal GetUserContentQueryResultMessage(ContentContainer usersContentsContainer)
        {
            ContentWithTags = usersContentsContainer.Values.ToDictionary(c => c.Uri, c => c.Tags);
        }
    }
}
