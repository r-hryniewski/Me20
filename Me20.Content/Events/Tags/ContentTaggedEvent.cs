using Me20.Common.Interfaces;
using System;

namespace Me20.Content.Events.Tags
{
    public class ContentTaggedEvent : IHaveContentUri
    {
        public Uri Uri { get; private set; }

        public ContentTaggedEvent(Uri uri)
        {
            Uri = uri;
        }
    }
}
