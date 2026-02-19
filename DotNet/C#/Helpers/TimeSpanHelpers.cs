using System;
using System.Collections.Generic;
using System.Globalization;

namespace 
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
			=> GetHumanReadableImpl(timeSpan, TimeUnit.Default, UnitName.Full, CultureInfo.CurrentCulture);

		public static string GetHumanReadable(TimeSpan timeSpan, CultureInfo cultureInfo)
			=> GetHumanReadableImpl(timeSpan, TimeUnit.Default, UnitName.Full, cultureInfo);

		public static string GetHumanReadable(TimeSpan timeSpan, TimeUnit timeUnit)
			=> GetHumanReadableImpl(timeSpan, timeUnit, UnitName.Full, CultureInfo.CurrentCulture);

		public static string GetHumanReadable(TimeSpan timeSpan, TimeUnit timeUnit, CultureInfo cultureInfo)
			=> GetHumanReadableImpl(timeSpan, timeUnit, UnitName.Full, cultureInfo);

		public static string GetHumanReadable(TimeSpan timeSpan, TimeUnit timeUnit, UnitName unitName)
			=> GetHumanReadableImpl(timeSpan, timeUnit, unitName, CultureInfo.CurrentCulture);

		public static string GetHumanReadable(TimeSpan timeSpan, TimeUnit timeUnit, UnitName unitName, CultureInfo cultureInfo)
			=> GetHumanReadableImpl(timeSpan, timeUnit, unitName, cultureInfo);

		private static string GetHumanReadableImpl(TimeSpan timeSpan, TimeUnit timeUnit, UnitName unitName, CultureInfo cultureInfo)
		{
			if (timeSpan == TimeSpan.Zero)
			{
				return "zero";
			}

			if (timeUnit == TimeUnit.None)
			{
				// G is [-]d:hh:mm:ss.fffffff

				return timeSpan.ToString("G", cultureInfo);
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
				string pluralizedDays = GetPluralizedUnit(TimeUnit.Days, unitName, days);

				string daysMessage = string.Format(cultureInfo, "{0}{1}", days, pluralizedDays);

				timeStrings.Add(daysMessage);
			}

			if (hours >= 1 && timeUnit.HasFlag(TimeUnit.Hours))
			{
				string pluralizedHours = GetPluralizedUnit(TimeUnit.Hours, unitName, hours);

				string hoursMessage = string.Format(cultureInfo, "{0}{1}", hours, pluralizedHours);

				timeStrings.Add(hoursMessage);
			}

			if (minutes >= 1 && timeUnit.HasFlag(TimeUnit.Minutes))
			{
				string pluralizedMinutes = GetPluralizedUnit(TimeUnit.Minutes, unitName, minutes);

				string minutesMessage = string.Format(cultureInfo, "{0}{1}", minutes, pluralizedMinutes);

				timeStrings.Add(minutesMessage);
			}

			if (seconds >= 1 && timeUnit.HasFlag(TimeUnit.Seconds))
			{
				string pluralizedSeconds = GetPluralizedUnit(TimeUnit.Seconds, unitName, seconds);

				string secondsMessage = string.Format(cultureInfo, "{0}{1}", seconds, pluralizedSeconds);

				timeStrings.Add(secondsMessage);
			}

			if (milliseconds >= 1 && timeUnit.HasFlag(TimeUnit.Milliseconds))
			{
				string pluralizedMilliseconds = GetPluralizedUnit(TimeUnit.Milliseconds, unitName, milliseconds);

				string millisecondsMessage = string.Format(cultureInfo, "{0}{1}", milliseconds, pluralizedMilliseconds);

				timeStrings.Add(millisecondsMessage);
			}

			if (microseconds >= 1 && timeUnit.HasFlag(TimeUnit.Microseconds))
			{
				string pluralizedMicroseconds = GetPluralizedUnit(TimeUnit.Microseconds, unitName, microseconds);

				string microsecondsMessage = string.Format(cultureInfo, "{0}{1}", microseconds, pluralizedMicroseconds);

				timeStrings.Add(microsecondsMessage);
			}

			if (nanoseconds >= 1 && timeUnit.HasFlag(TimeUnit.Nanoseconds))
			{
				string pluralizedNanoseconds = GetPluralizedUnit(TimeUnit.Nanoseconds, unitName, nanoseconds);

				string nanosecondsMessage = string.Format(cultureInfo, "{0}{1}", nanoseconds, pluralizedNanoseconds);

				timeStrings.Add(nanosecondsMessage);
			}

			if (ticks >= 1 && timeUnit.HasFlag(TimeUnit.Ticks))
			{
				string pluralizedTicks = GetPluralizedUnit(TimeUnit.Ticks, unitName, ticks);

				string ticksMessage = string.Format(cultureInfo, "{0}{1}", ticks, pluralizedTicks);

				timeStrings.Add(ticksMessage);
			}

			if (timeStrings.Count == 0)
			{
				// G is [-]d:hh:mm:ss.fffffff

				return timeSpan.ToString("G", cultureInfo);
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
