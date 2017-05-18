using System;

namespace Me20.Identity.Events
{
    public class UserContentRemovedEvent
    {
        public Uri Uri { get; private set; }

        public UserContentRemovedEvent(Uri uri)
        {
            Uri = uri;
        }
    }
}
