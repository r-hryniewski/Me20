using Me20.Common.Abstracts;
using System;
using System.Collections.Generic;

namespace Me20.Common.Commands
{
    public class AddContentCommand : CommandBase
    {
        public Uri ContentUri { get; private set; }
        public IEnumerable<string> ContentTags { get; private set; }
        public string SendeeUserName { get; private set; }

        public AddContentCommand(Uri uri, string userName, IEnumerable<string> contentTags = null) : base()
        {
            ContentUri = uri;
            SendeeUserName = userName;
            ContentTags = contentTags;
        }
    }
}
