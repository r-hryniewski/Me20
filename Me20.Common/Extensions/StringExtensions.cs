using System;
using System.Text;

namespace Me20.Common.Extensions
{
    public static class StringExtensions
    {
        [Obsolete("Base64 has slashes so it's useless to ActorPaths, use MD5 instead")]
        public static string ToBase64(this string input) => Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        [Obsolete("Base64 has slashes so it's useless to ActorPaths, use MD5 instead")]
        public static string FromBase64(this string input) => Encoding.UTF8.GetString(Convert.FromBase64String(input));
    }
}
