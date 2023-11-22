using System;
using System.Collections.Generic;
using System.Linq;
using static System.Security.Cryptography.RandomNumberGenerator;

namespace .Extensions
{
	public static class LinqExtensions
	{
		public static IEnumerable<T> Randomise<T>(this IEnumerable<T> enumerable)
		{
			return enumerable.OrderBy(static x => GetInt32(Int32.MaxValue));
		}
	}
}
