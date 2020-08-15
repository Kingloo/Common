using System;

namespace .Common
{
    public static class TimeSpanMaths
    {
        public static TimeSpan Multiply(TimeSpan multiplicand, int multiplier)
        {
            return TimeSpan.FromTicks(multiplicand.Ticks * multiplier);
        }

        public static TimeSpan Pow(TimeSpan powerBase, int powerExponent)
        {
            return TimeSpan.FromTicks(powerBase.Ticks ^ powerExponent);
        }
    }
}