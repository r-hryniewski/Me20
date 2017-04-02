using System;
using System.Collections.Generic;

namespace Me20.Identity.Events
{
    internal class ContentAddedEvent
    {
        internal Uri ContentUri { get; private set; }
        internal IEnumerable<string> ContentTags { get; private set; }

        internal ContentAddedEvent(Uri uri, IEnumerable<string> contentTags = null)
        {
            ContentUri = uri;
            ContentTags = contentTags;
        }
    }
}
