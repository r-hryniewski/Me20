using Me20.Common.Interfaces;
using System;

namespace Me20.Common.Extensions
{
    public static class IHaveContentUriExtensions
    {
        public static string SchemalessUriAsMD5(this IHaveContentUri item) => item?.Uri?.ToSchemalessUriAsMD5();
    }
}
