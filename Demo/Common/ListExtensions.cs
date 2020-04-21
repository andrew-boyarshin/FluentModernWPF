using System;
using System.Collections.Generic;

namespace CodeWanda.UI.Common
{
    public static class ListExtensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> collection)
        {
            if (list == null)
                throw new ArgumentNullException(nameof (list));
            if (collection == null)
                throw new ArgumentNullException(nameof (collection));
            if (list is List<T> objList)
            {
                objList.AddRange(collection);
            }
            else
            {
                foreach (var obj in collection)
                    list.Add(obj);
            }
        }

        public static int RemoveAll<T>(this IList<T> list, Predicate<T> match)
        {
            if (list == null)
                throw new ArgumentNullException(nameof (list));
            if (match == null)
                throw new ArgumentNullException(nameof (match));
            if (list is List<T> objList)
                return objList.RemoveAll(match);
            var num = 0;
            for (var index = list.Count - 1; index >= 0; --index)
            {
                if (match(list[index]))
                {
                    list.RemoveAt(index);
                    ++num;
                }
            }
            return num;
        }
    }
}