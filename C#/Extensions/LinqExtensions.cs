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
            string ticks = DateTimeOffset.Now.Ticks.ToString();

            int numberOfDigits = Int32.Parse(ticks[^1..] + 1);

            Int64 seed64 = Int64.Parse(ticks[^numberOfDigits..]);

            return (seed64 < Int32.MaxValue) ? Convert.ToInt32(seed64) : Int32.MaxValue;
        }
    }
}