using Me20.Common.Interfaces;
using System;

namespace Me20.Common.Extensions
{
    public static class IHaveContentUriExtensions
    {
        [Obsolete("Base64 has slashes so it's useless to ActorPaths, use MD5 instead")]
        public static string SchemalessUriAsBase64(this IHaveContentUri item) => item?.Uri?.ToSchemalessUriAsBase64();

        public static string SchemalessUriAsMD5(this IHaveContentUri item) => item?.Uri?.ToSchemalessUriAsMD5();
    }
}
