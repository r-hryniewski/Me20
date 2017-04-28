using System;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Identity.Events
{
    public class ContentTaggedByUserEvent
    {
        public Uri ContentUri { get; private set; }
        public string[] ContentTags { get; private set; }

        public ContentTaggedByUserEvent(Uri uri, IEnumerable<string> contentTags = null)
        {
            ContentUri = uri;
            ContentTags = contentTags?.ToArray();
        }
    }
}
