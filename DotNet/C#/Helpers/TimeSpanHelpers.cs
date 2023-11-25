using System;
using System.Collections.Generic;

namespace .Common
{
	public static class TimeSpanHelpers
	{
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
		{
			if (timeSpan == TimeSpan.Zero)
			{
				return "0 seconds";
			}

			List<string> timeStrings = new List<string>();

			int days = timeSpan.Days;
			int hours = timeSpan.Hours;
			int minutes = timeSpan.Minutes;
			int seconds = timeSpan.Seconds;

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

			return String.Join(" ", timeStrings);
		}
	}
}
