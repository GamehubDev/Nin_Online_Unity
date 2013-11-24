using System;
using System.Collections.Generic;

namespace CrystalLib.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            return val.CompareTo(max) > 0 ? max : val;
        }

        public static bool IsEqual<T>(this IList<T> list, IList<T> target, IComparer<T> comparer) where T : IComparable<T>
        {
            if (list.Count != target.Count)
            {
                return false;
            }
            int index = 0;
            while (index < list.Count &&
                   comparer.Compare(list[index], target[index]) == 0)
            {
                index++;
            }
            if (index != list.Count)
            {
                return false;
            }
            return true;
        }
    }
}
