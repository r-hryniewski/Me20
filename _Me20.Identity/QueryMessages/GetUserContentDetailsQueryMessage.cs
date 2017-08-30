using Me20.Common.Interfaces;
using System;

namespace Me20.Identity.QueryMessages
{
    public class GetUserContentDetailsQueryMessage : IHaveUserName, IHaveContentUri
    {
        public string UserName { get; private set; }
        public Uri Uri { get; private set; }

        public GetUserContentDetailsQueryMessage(string userName, Uri uri)
        {
            UserName = userName;
            Uri = uri;
        }
    }
}
