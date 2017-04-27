using System;

namespace Me20.Common.Extensions
{
    public static class UriExtensions
    {
        public static string WithoutSchema(this Uri uri) => uri?.GetComponents(UriComponents.AbsoluteUri &~UriComponents.Scheme, UriFormat.UriEscaped) ?? string.Empty;

        [Obsolete("Base64 has slashes so it's useless to ActorPaths, use MD5 instead")]
        public static string ToSchemalessUriAsBase64(this Uri uri) => uri?.WithoutSchema().ToLower().ToBase64();

        public static string ToSchemalessUriAsMD5(this Uri uri) => uri?.WithoutSchema().ToLower().ToMD5();
    }
}
