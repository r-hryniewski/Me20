using System;
using System.Collections.Generic;

namespace Me20.Content.Events.Contents
{
    public class ContentTaggedWithEvent
    {
        public HashSet<string> Tags { get; private set; }

        public ContentTaggedWithEvent(IEnumerable<string> tagNames)
        {
            Tags = new HashSet<string>(tagNames, StringComparer.OrdinalIgnoreCase);
        }
    }
}
