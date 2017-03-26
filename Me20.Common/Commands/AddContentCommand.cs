using Me20.Common.Abstracts;
using System.Collections.Generic;

namespace Me20.Common.Commands
{
    public class AddContentCommand : CommandBase
    {
        public string ContentUrl { get; private set; }
        public IEnumerable<string> ContentTags { get; private set; }

        public AddContentCommand(string url, IEnumerable<string> contentTags = null) : base()
        {
            ContentUrl = url;
            ContentTags = contentTags;
        }
    }
}
