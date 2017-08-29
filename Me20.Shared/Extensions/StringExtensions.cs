using System;
using System.Security.Cryptography;
using System.Text;

namespace Me20.Shared.Extensions
{
    public static class StringExtensions
    {
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
