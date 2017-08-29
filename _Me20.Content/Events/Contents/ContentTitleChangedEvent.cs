using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Content.Events.Contents
{
    public class ContentTitleChangedEvent
    {
        public string Title { get; private set; }
        public ContentTitleChangedEvent(string title)
        {
            Title = title;
        }
    }
}
