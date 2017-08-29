using Me20.Common.Interfaces;
using System;

namespace Me20.Content.QueryMessages
{
    public class GetContentDetailsQueryMessage : IHaveUserName, IHaveContentUri
    {
        public string UserName { get; private set; }
        public Uri Uri { get; private set; }

        public GetContentDetailsQueryMessage(string userName, Uri uri)
        {
            UserName = userName;
            Uri = uri;
        }
    }
}
