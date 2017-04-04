using System;
using System.Collections.Generic;

namespace Me20.Identity.Events
{
    public class ContentAddedEvent
    {
        public Uri ContentUri { get; private set; }
        public IEnumerable<string> ContentTags { get; private set; }

        public ContentAddedEvent(Uri uri, IEnumerable<string> contentTags = null)
        {
            ContentUri = uri;
            ContentTags = contentTags;
        }
    }
}
