using Me20.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Me20.Common.Comparers
{
    [Obsolete("Base64 has slashes so it's useless to ActorPaths, use MD5 instead")]
    public class SchemalessBase64UriComparer : IEqualityComparer<Uri>
    {
        public bool Equals(Uri x, Uri y) => x.ToSchemalessUriAsBase64().Equals(y.ToSchemalessUriAsBase64());

        public int GetHashCode(Uri obj) => obj.ToSchemalessUriAsBase64().GetHashCode();
    }
}
