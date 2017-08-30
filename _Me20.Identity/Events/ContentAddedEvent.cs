using System;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Identity.Events
{
    public class ContentAddedEvent
    {
        public Uri ContentUri { get; private set; }
        public string Title { get; private set; }
        public string[] ContentTags { get; private set; }
        public DateTime Added { get; private set; }

        public ContentAddedEvent(Uri uri, string title, IEnumerable<string> contentTags = null)
        {
            ContentUri = uri;
            Title = !string.IsNullOrEmpty(title) ? title : uri.ToString();
            ContentTags = contentTags?.ToArray();
            Added = DateTime.UtcNow;
        }
    }
}
