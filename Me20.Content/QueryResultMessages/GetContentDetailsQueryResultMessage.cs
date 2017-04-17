using System;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Content.QueryResultMessages
{
    public class GetContentDetailsQueryResultMessage
    {
        public Uri Uri { get; private set; }
        public double AverageRating { get; private set; }
        public byte Rating { get; private set; }
        public IReadOnlyCollection<string> Tags { get; private set; }

        public GetContentDetailsQueryResultMessage(Uri uri, double averageRating, ISet<string> tags, byte rating)
        {
            Uri = uri;
            AverageRating = averageRating;
            Rating = rating;
            Tags = tags.ToArray();
        }
    }
}
