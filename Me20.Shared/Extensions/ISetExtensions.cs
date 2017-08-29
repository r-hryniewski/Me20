using System.Collections.Generic;

namespace Me20.Shared.Extensions
{
    public static class ISetExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="items"></param>
        /// <returns>Number of items that was successfully added</returns>
        public static long AddRange<T>(this ISet<T> source, IEnumerable<T> items)
        {
            if (source == null || items.IsNullOrEmpty())
                return 0;

            var successfullyAdded = 0L;

            foreach (var item in items)
                if (source.Add(item))
                    successfullyAdded++;

            return successfullyAdded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="items"></param>
        /// <returns>True if any element was added sucessfully</returns>
        public static bool AddRangeAny<T>(this ISet<T> source, IEnumerable<T> items)
        {
            if (source == null || items.IsNullOrEmpty())
                return false;

            var anyAdded = false;

            foreach (var item in items)
                anyAdded |= source.Add(item);

            return anyAdded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="items"></param>
        /// <returns>True if all elements was added sucessfully</returns>
        public static bool AddRangeAll<T>(this ISet<T> source, IEnumerable<T> items)
        {
            if (source == null || items.IsNullOrEmpty())
                return false;

            var allAdded = true;

            foreach (var item in items)
                allAdded &= source.Add(item);

            return allAdded;
        }
    }
}
