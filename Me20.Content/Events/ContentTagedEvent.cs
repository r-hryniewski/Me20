using System;
using System.Collections.Generic;

namespace Me20.Content.Events
{
    public class ContentTagedEvent
    {
        public HashSet<string> Tags { get; private set; }

        public ContentTagedEvent(IEnumerable<string> tagNames)
        {
            Tags = new HashSet<string>(tagNames, StringComparer.OrdinalIgnoreCase);
        }
    }
}
