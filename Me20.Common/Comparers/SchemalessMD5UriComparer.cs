using Me20.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Me20.Common.Comparers
{
    public class SchemalessMD5UriComparer : IEqualityComparer<Uri>
    {
        public bool Equals(Uri x, Uri y) => x.ToSchemalessUriAsMD5().Equals(y.ToSchemalessUriAsMD5());

        public int GetHashCode(Uri obj) => obj.ToSchemalessUriAsMD5().GetHashCode();
    }
}
