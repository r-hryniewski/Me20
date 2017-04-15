using System;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Identity.Models
{
    internal class UsersContent
    {
        internal Uri Uri { get; private set; }
        internal HashSet<string> Tags { get; private set; }
        internal byte Rating { get; private set; }

        internal UsersContent(Uri contentUri)
        {
            Uri = contentUri;
            Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        internal UsersContent(Uri contentUri, IEnumerable<string> tags) : this(contentUri)
        {
            if (tags != null)
                Tags = new HashSet<string>(tags, StringComparer.OrdinalIgnoreCase);
        }


        internal bool UpdateTags(IEnumerable<string> contentTags) => contentTags.Select(t => Tags.Add(t)).Any(t => t);

        internal void Rate(byte rating)
        {
            Rating = rating;
        }
    }
}
