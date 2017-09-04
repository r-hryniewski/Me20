using Me20.Contracts.Entities;
using Me20.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.IdentityActors.ValueObjects
{
    public class UsersContent : IContent
    {
        public string Id => ContentUri.ToSchemalessUriAsMD5();

        public Uri ContentUri { get; private set; }

        public IEnumerable<string> Tags { get; private set; }

        public bool IsValid => ContentUri != null;

        public UsersContent(Uri uri, IEnumerable<string> tags)
        {
            ContentUri = uri;
            Tags = tags;
        }
    }
}
