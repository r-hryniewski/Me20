
using System;

namespace Me20.Common.Messages
{
    public class CreateContentIfNotExistsMessage
    {
        public Uri Uri { get; private set; }

        public CreateContentIfNotExistsMessage(Uri uri)
        {
            Uri = uri;
        }
    }
}
