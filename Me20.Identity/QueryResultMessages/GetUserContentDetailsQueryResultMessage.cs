using Me20.Common.Interfaces;
using Me20.Identity.Models;
using System;
using System.Collections.Generic;

namespace Me20.Identity.QueryResultMessages
{
    public class GetUserContentDetailsQueryResultMessage : IHaveContentUri
    {
        public string Title { get; private set; }
        public byte Rating { get; private set; }
        public IReadOnlyCollection<string> Tags { get; private set; }
        public Uri Uri { get; private set; }

        internal GetUserContentDetailsQueryResultMessage(UsersContent usersContent)
        {
            Rating = usersContent.Rating;
            Tags = usersContent.Tags;
            Uri = usersContent.Uri;
            Title = usersContent.Title;
        }
    }
}
