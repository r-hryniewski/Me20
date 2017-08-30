using System;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Identity.Models
{
    internal class UsersContent
    {
        internal Uri Uri { get; private set; }
        internal string Title { get; private set; }
        internal HashSet<string> Tags { get; private set; }
        internal byte Rating { get; private set; }
        internal DateTime Added { get; private set; }

        internal UsersContent(Uri contentUri, string title)
        {
            Uri = contentUri;
            Title = !string.IsNullOrEmpty(title) ? title : contentUri.ToString();
            Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Added = DateTime.UtcNow;
        }

        internal UsersContent(Uri contentUri, string title, IEnumerable<string> tags) : this(contentUri, title)
        {
            if (tags != null)
                Tags = new HashSet<string>(tags, StringComparer.OrdinalIgnoreCase);
        }
        internal UsersContent(Uri contentUri, string title, IEnumerable<string> tags, DateTime added) : this(contentUri, title, tags)
        {
            Added = added;
        }


        internal bool UpdateTags(IEnumerable<string> contentTags) => contentTags.Select(t => Tags.Add(t)).Any(t => t);

        internal bool Rename(string title)
        {
            if (Title.Equals(title))
                return false;

            Title = title;
            return true;
        }

        internal void Rate(byte rating)
        {
            Rating = rating;
        }
    }
}
