using System;
using System.Collections.Generic;

namespace .Helpers
{
	public static class TimeSpanHelpers
	{
		public enum TimeUnit
		{
			None,
			All,
			Milliseconds,
			Seconds,
			Minutes,
			Hours,
			Days,
			Unknown
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
			=> GetHumanReadableImpl(timeSpan, TimeUnit.All);

		public static string GetHumanReadable(TimeSpan timeSpan, TimeUnit timeUnit)
			=> GetHumanReadableImpl(timeSpan, timeUnit);

		private static string GetHumanReadableImpl(TimeSpan timeSpan, TimeUnit timeUnit)
		{
			if (timeSpan == TimeSpan.Zero)
			{
				return "zero";
			}

			return timeUnit switch
			{
				TimeUnit.All => FormatWithEveryTimeUnit(timeSpan),
				TimeUnit.Days => $"{timeSpan.TotalDays} {(timeSpan.TotalDays == 1 ? "day" : "days")}",
				TimeUnit.Hours => $"{timeSpan.TotalHours} {(timeSpan.TotalHours == 1 ? "hour" : "hours")}",
				TimeUnit.Minutes => $"{timeSpan.TotalMinutes} {(timeSpan.TotalMinutes == 1 ? "minute" : "minutes")}",
				TimeUnit.Seconds => $"{timeSpan.TotalSeconds} {(timeSpan.TotalSeconds == 1 ? "second" : "second")}",
				TimeUnit.Milliseconds => $"{timeSpan.TotalMilliseconds} {(timeSpan.TotalMilliseconds == 1 ? "millisecond" : "milliseconds")}",
				TimeUnit.Unknown => "unknown",
				_ => timeSpan.ToString()
			};
		}

		private static string FormatWithEveryTimeUnit(TimeSpan timeSpan)
		{
			List<string> timeStrings = new List<string>(capacity: 5);

			int days = timeSpan.Days;
			int hours = timeSpan.Hours;
			int minutes = timeSpan.Minutes;
			int seconds = timeSpan.Seconds;
			int milliseconds = timeSpan.Milliseconds;

			if (days >= 1)
			{
				timeStrings.Add($"{days} {(days == 1 ? "day" : "days")}");
			}

			if (hours >= 1)
			{
				timeStrings.Add($"{hours} {(hours == 1 ? "hour" : "hours")}");
			}

			if (minutes >= 1)
			{
				timeStrings.Add($"{minutes} {(minutes == 1 ? "minute" : "minutes")}");
			}

			if (seconds >= 1)
			{
				timeStrings.Add($"{seconds} {(seconds == 1 ? "second" : "seconds")}");
			}

			if (milliseconds >= 1)
			{
				timeStrings.Add($"{milliseconds} {(milliseconds == 1 ? "millisecond" : "milliseconds")}");
			}

			return String.Join(' ', timeStrings);
		}
	}
}
