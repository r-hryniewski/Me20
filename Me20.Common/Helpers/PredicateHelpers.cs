using System;
using System.Linq;

namespace Me20.Common.Helpers
{
    public static class PredicateHelpers
    {
        public static bool AllAre<T>(Predicate<T> predicate, params T[] items) => items.All(i => predicate(i));

        public static bool AnyAre<T>(Predicate<T> predicate, params T[] items) => items.Any(i => predicate(i));
    }
}
