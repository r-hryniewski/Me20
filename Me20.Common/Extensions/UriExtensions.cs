﻿using System;

namespace Me20.Common.Extensions
{
    public static class UriExtensions
    {
        public static string WithoutSchema(this Uri uri) => uri?.GetComponents(UriComponents.AbsoluteUri &~UriComponents.Scheme, UriFormat.UriEscaped) ?? string.Empty;

        public static string ToSchemalessUriAsMD5(this Uri uri) => uri?.WithoutSchema().ToLower().ToMD5();
    }
}
