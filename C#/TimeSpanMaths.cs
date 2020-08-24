using System;

namespace 
{
    public static class TimeSpanMaths
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
    }
}