using Me20.Common.Abstracts;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Core.Contents
{
    public class Content : HaveDispatchersBase<Content>
    {
        public string Url { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public Content() : base()
        {
           
        }
    }
}
