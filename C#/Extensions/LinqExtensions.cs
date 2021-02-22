using System;
using System.Collections.Generic;
using System.Linq;

namespace .Extensions
{
    public static class LinqExtensions
    {
        public static IList<T> Randomise<T>(this IList<T> list)
        {
            Random random = new Random(GetSeed());

            return list.OrderBy(x => random.Next()).ToList();
        }

        public static IEnumerable<T> Randomise<T>(this IEnumerable<T> enumerable)
        {
            Random random = new Random(GetSeed());

            return enumerable.ToList().OrderBy(x => random.Next());
        }

        private static int GetSeed()
        {
            // the "+ 1" ensures we never multiply by zero

            int clockDigitsToTake = Math.Min(4, Int32.Parse(DateTimeOffset.Now.Ticks.ToString()[^1..]) + 1);
            int uptimeDigitsToTake = Math.Min(3, Int32.Parse(Environment.TickCount64.ToString()[^1..]) + 1);
            int systemPageSizeDigitsToTake = Math.Min(4, Int32.Parse(Environment.SystemPageSize.ToString()[^1..]) + 1);
            int workingSetDigitsToTake = Math.Min(3, Int32.Parse(Environment.WorkingSet.ToString()[^1..]) + 1);

            Int64 clockDigits = Int64.Parse(DateTimeOffset.Now.Ticks.ToString()[^clockDigitsToTake..]) + 1;
            Int64 uptimeDigits = Int64.Parse(Environment.TickCount64.ToString()[^uptimeDigitsToTake..]) + 1;
            Int64 systemPageSizeDigits = Int64.Parse(Environment.SystemPageSize.ToString()[^systemPageSizeDigitsToTake..]) + 1;
            Int64 workingSetDigits = Int64.Parse(Environment.WorkingSet.ToString()[^workingSetDigitsToTake..]) + 1;

            Int64 seed64 = (clockDigits * uptimeDigits) + (systemPageSizeDigits * workingSetDigits);

            try
            {
                return Convert.ToInt32(seed64);
            }
            catch (OverflowException)
            {
                return Int32.Parse(Environment.TickCount.ToString()[^5..]);
            }
        }
    }
}