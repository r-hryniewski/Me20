using System.Collections.Generic;
using System.Linq;

namespace Me20.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => source == null || !source.Any();
    }
}
