using System;
using System.Collections.Generic;
using System.Linq;

namespace .Extensions
{
	public static class LinqExtensions
	{
		public static IList<T> Randomise<T>(this IList<T> list)
		{
			Random random = new Random();

			return list.OrderBy(x => random.Next()).ToList();
		}

		public static IEnumerable<T> Randomise<T>(this IEnumerable<T> enumerable)
		{
			Random random = new Random();

			return enumerable.ToList().OrderBy(x => random.Next());
		}
	}
}
