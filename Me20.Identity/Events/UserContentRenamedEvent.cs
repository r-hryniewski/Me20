using System;

namespace Me20.Identity.Events
{
    public class UserContentRenamedEvent
    {
        public string Title { get; private set; }
        public Uri Uri { get; private set; }

        public UserContentRenamedEvent(Uri uri, string title)
        {
            Title = title;
            Uri = uri;
        }
    }
}
