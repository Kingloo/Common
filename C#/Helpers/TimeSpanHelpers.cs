using System;
using System.Globalization;

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

        [System.Diagnostics.DebuggerStepThrough]
        public static string GetHumanReadable(int timestampSeconds)
            => GetHumanReadable(TimeSpan.FromSeconds(timestampSeconds));

        [System.Diagnostics.DebuggerStepThrough]
        public static string GetHumanReadableMs(int timestampMs)
            => GetHumanReadable(TimeSpan.FromMilliseconds(timestampMs));

        [System.Diagnostics.DebuggerStepThrough]
        public static string GetHumanReadable(TimeSpan timeSpan)
            => GetHumanReadable(timeSpan, CultureInfo.CurrentCulture);

        public static string GetHumanReadable(TimeSpan timeSpan, CultureInfo ci)
        {
            const int oneDay = 86400;
            const int oneHour = 3600;
            const int oneMinute = 60;

            return timeSpan switch
            {
                _ when timeSpan.TotalSeconds >= oneDay => string.Format(ci, "{0:%d} days {0:%h} hours {0:%m} minutes {0:%s} seconds", timeSpan),
                _ when timeSpan.TotalSeconds >= oneHour => string.Format(ci, "{0:%h} hours {0:%m} minutes {0:%s} seconds", timeSpan),
                _ when timeSpan.TotalSeconds >= oneMinute => string.Format(ci, "{0:%m} minutes {0:%s} seconds", timeSpan),
                _ => string.Format(ci, "{0:%s} seconds", timeSpan)
            };
        }
    }
}