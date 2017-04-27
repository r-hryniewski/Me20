using System;
using System.Security.Cryptography;
using System.Text;

namespace Me20.Common.Extensions
{
    public static class StringExtensions
    {
        [Obsolete("Base64 has slashes so it's useless to ActorPaths, use MD5 instead")]
        public static string ToBase64(this string input) => Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        [Obsolete("Base64 has slashes so it's useless to ActorPaths, use MD5 instead")]
        public static string FromBase64(this string input) => Encoding.UTF8.GetString(Convert.FromBase64String(input));

        public static string ToMD5(this string input)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            if (string.IsNullOrWhiteSpace(input))
                return input;

            using (MD5 md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.Default.GetBytes(input));

                var sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
