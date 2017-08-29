using Me20.Contracts;
using System;

namespace Me20.Shared.Extensions
{
    public static class IHaveContentUriExtensions
    {
        public static string SchemalessUriAsMD5(this IHaveContentUri item) => item?.ContentUri?.ToSchemalessUriAsMD5();
    }
}
