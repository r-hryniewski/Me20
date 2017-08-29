using System.Collections.Generic;
using System.Linq;

namespace Me20.Content.Events.Contents
{
    public class ContentTaggedWithEvent
    {
        public string[] Tags { get; private set; }

        public ContentTaggedWithEvent(IEnumerable<string> tagNames)
        {
            Tags = tagNames?.ToArray() ?? new string[0];
        }
    }
}
