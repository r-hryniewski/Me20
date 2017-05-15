using Me20.Common.Abstracts;
using Me20.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Me20.Common.Commands
{
    public class AddContentCommand : CommandBase, IHaveUserName, IHaveContentUri, IHaveContentTags
    {
        public Uri Uri { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<string> ContentTags { get; private set; }
        public string UserName { get; private set; }

        public AddContentCommand(Uri uri, string title, string userName, IEnumerable<string> contentTags = null) : base()
        {
            Uri = uri;
            Title = !string.IsNullOrEmpty(title) ? title : uri.ToString();
            UserName = userName;
            ContentTags = contentTags;
        }
    }
}
