using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Identity.Events
{
    public class ContentRatedEvent
    {
        public Uri Uri { get; private set; }
        public byte Rating { get; private set; }

        public ContentRatedEvent(Uri uri, byte rating)
        {
            Uri = uri;
            Rating = rating;
        }
    }
}
