using Me20.Common.Abstracts;
using Me20.Common.Interfaces;
using System;

namespace Me20.Common.Commands
{
    public class RenameUserContentCommand : CommandBase, IHaveUserName, IHaveContentUri 
    {
        public Uri Uri { get; private set; }
        public string Title { get; private set; }
        public string UserName { get; private set; }

        public RenameUserContentCommand
            (Uri uri, string userName, string title) : base()
        {
            Uri = uri;
            UserName = userName;
            Title = title;
        }
    }
}
