using System;
using System.Collections.Generic;
using System.Globalization;

namespace .Helpers
{
	public static class TimeSpanHelpers
	{
		[Flags]
		public enum TimeUnit : int
		{
			None			= 0,
			Ticks			= 1,
			Nanoseconds		= 2,
			Microseconds	= 4,
			Milliseconds	= 8,
			Seconds			= 16,
			Minutes			= 32,
			Hours			= 64,
			Days			= 128,
			Default = Milliseconds | Seconds | Minutes | Hours,
			All = Ticks | Nanoseconds | Microseconds | Milliseconds | Seconds | Minutes | Hours | Days
		}

		public enum UnitName
		{
			Full,
			Abbreviation
		}

		public static TimeSpan Multiply(TimeSpan multiplicand, int multiplier)
		{
			return TimeSpan.FromTicks(multiplicand.Ticks * multiplier);
		}

		public static TimeSpan Pow(TimeSpan timeSpan, double exponent)
		{
			double newTimeSecs = Math.Pow(timeSpan.TotalSeconds, exponent);

			return TimeSpan.FromSeconds(newTimeSecs);
		}

		public static string GetHumanReadable(TimeSpan timeSpan)
			=> GetHumanReadableImpl(timeSpan, TimeUnit.Default, UnitName.Full);

		public static string GetHumanReadable(TimeSpan timeSpan, TimeUnit timeUnit)
			=> GetHumanReadableImpl(timeSpan, timeUnit, UnitName.Full);

		public static string GetHumanReadable(TimeSpan timeSpan, TimeUnit timeUnit, UnitName unitName)
			=> GetHumanReadableImpl(timeSpan, timeUnit, unitName);

		private static string GetHumanReadableImpl(TimeSpan timeSpan, TimeUnit timeUnit, UnitName unitName)
		{
			if (timeSpan == TimeSpan.Zero)
			{
				return "zero";
			}

			if (timeUnit == TimeUnit.None)
			{
				return timeSpan.ToString("G", CultureInfo.CurrentCulture);
			}

			List<string> timeStrings = new List<string>(capacity: 5);

			int days = timeSpan.Days;
			int hours = timeSpan.Hours;
			int minutes = timeSpan.Minutes;
			int seconds = timeSpan.Seconds;
			int milliseconds = timeSpan.Milliseconds;
			int microseconds = timeSpan.Microseconds;
			int nanoseconds = timeSpan.Nanoseconds;
			long ticks = timeSpan.Ticks;

			if (days >= 1 && timeUnit.HasFlag(TimeUnit.Days))
			{
				timeStrings.Add($"{days}{GetPluralizedUnit(TimeUnit.Days, unitName, days)}");
			}

			if (hours >= 1 && timeUnit.HasFlag(TimeUnit.Hours))
			{
				timeStrings.Add($"{hours}{GetPluralizedUnit(TimeUnit.Hours, unitName, hours)}");
			}

			if (minutes >= 1 && timeUnit.HasFlag(TimeUnit.Minutes))
			{
				timeStrings.Add($"{minutes}{GetPluralizedUnit(TimeUnit.Minutes, unitName, minutes)}");
			}

			if (seconds >= 1 && timeUnit.HasFlag(TimeUnit.Seconds))
			{
				timeStrings.Add($"{seconds}{GetPluralizedUnit(TimeUnit.Seconds, unitName, seconds)}");
			}

			if (milliseconds >= 1 && timeUnit.HasFlag(TimeUnit.Milliseconds))
			{
				timeStrings.Add($"{milliseconds}{GetPluralizedUnit(TimeUnit.Milliseconds, unitName, milliseconds)}");
			}

			if (microseconds >= 1 && timeUnit.HasFlag(TimeUnit.Microseconds))
			{
				timeStrings.Add($"{microseconds}{GetPluralizedUnit(TimeUnit.Microseconds, unitName, microseconds)}");
			}

			if (nanoseconds >= 1 && timeUnit.HasFlag(TimeUnit.Nanoseconds))
			{
				timeStrings.Add($"{nanoseconds}{GetPluralizedUnit(TimeUnit.Nanoseconds, unitName, nanoseconds)}");
			}

			if (ticks >= 1 && timeUnit.HasFlag(TimeUnit.Ticks))
			{
				timeStrings.Add($"{ticks}{GetPluralizedUnit(TimeUnit.Ticks, unitName, ticks)}");
			}

			return String.Join(' ', timeStrings);
		}

		private static string GetPluralizedUnit(TimeUnit timeUnit, UnitName unitName, long length)
		{
			return timeUnit switch
			{
				TimeUnit.Ticks => unitName switch
				{
					UnitName.Abbreviation => "ts",
					_ => length == 1 ? " tick" : " ticks"
				},
				TimeUnit.Nanoseconds => unitName switch
				{
					UnitName.Abbreviation => "ns",
					_ => length == 1 ? " nanosecond" : " nanoseconds"
				},
				TimeUnit.Microseconds => unitName switch
				{
					UnitName.Abbreviation => "μs",
					_ => length == 1 ? " microsecond" : " microseconds"
				},
				TimeUnit.Milliseconds => unitName switch
				{
					UnitName.Abbreviation => "ms",
					_ => length == 1 ? " millisecond" : " milliseconds"
				},
				TimeUnit.Seconds => unitName switch
				{
					UnitName.Abbreviation => "s",
					_ => length == 1 ? " second" : " seconds"
				},
				TimeUnit.Minutes => unitName switch
				{
					UnitName.Abbreviation => "m",
					_ => length == 1 ? " minute" : " minutes"
				},
				TimeUnit.Hours => unitName switch
				{
					UnitName.Abbreviation => "h",
					_ => length == 1 ? " hour" : " hours"
				},
				TimeUnit.Days => unitName switch
				{
					UnitName.Abbreviation => "d",
					_ => length == 1 ? " day" : " days"
				},
				_ => throw new ArgumentException($"invalid TimeUnit: '{timeUnit}'", nameof(timeUnit))
			};
		}
	}
}
