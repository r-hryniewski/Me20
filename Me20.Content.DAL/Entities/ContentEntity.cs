using Me20.Contracts.Entities;
using Me20.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Content.DAL.Entities
{
    public class ContentEntity : IContent
    {
        public string Id => ContentUri.ToSchemalessUriAsMD5();

        public string Url { get; private set; }
        public IEnumerable<string> Tags { get; private set; }

        private Uri uri;
        public Uri ContentUri
        {
            get
            {
                try
                {
                    return uri ?? (uri = new UriBuilder(Url).Uri);
                }
                catch (Exception)
                {
                    //TODO: Log exception
                    return null;
                }
            }
        }

        public bool IsValid => ContentUri != null;

        public ContentEntity(string url, IEnumerable<string> tags)
        {
            Url = url;
            Tags = tags;
        }

        public ContentEntity(Uri uri, IEnumerable<string> tags) 
            : this(url: uri.ToString(),
                  tags: tags)
        {}
    }
}
