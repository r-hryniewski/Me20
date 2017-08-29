using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Me20.ApiGateway.Extensions
{
    public static class UriExtensions
    {
        public static string WithoutSchema(this Uri uri) => uri?.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Scheme, UriFormat.UriEscaped) ?? string.Empty;

        public static string ToSchemalessUriAsMD5(this Uri uri) => uri?.WithoutSchema().ToLower().ToMD5();
    }
}