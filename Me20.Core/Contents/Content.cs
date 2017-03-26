using Me20.Common.Abstracts;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Core.Contents
{
    public class Content : HaveDispatchersBase<Content>
    {
        public string Url { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public Content(string url, IEnumerable<string> tags) : base()
        {
            //TODO: Parse and validate url
            Url = url;
            Tags = tags;
        }

        public Content(string url, params string[] tags) : this(url, tags.AsEnumerable())
        { }
    }
}
