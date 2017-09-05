using Me20.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Shared.Extensions
{
    public static class TagExtensions
    {
        public static string TagNameToId(this IHaveTagName item) => TagNameToId(item.TagName);

        public static string TagNameToId(string tagName) => tagName.ToLowerInvariant().ToMD5();
    }
}
