using Me20.Common.Abstracts;
using Me20.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Me20.Common.Commands
{
    public class TagContentCommand : CommandBase, IHaveUserName, IHaveContentUri, IHaveContentTags
    {
        public Uri Uri { get; private set; }
        public IEnumerable<string> ContentTags { get; private set; }
        public string UserName { get; private set; }

        public TagContentCommand(Uri uri, string userName, IEnumerable<string> contentTags = null) : base()
        {
            Uri = uri;
            UserName = userName;
            ContentTags = contentTags;
        }
    }
}
