using System;
using System.Globalization;

namespace .Extensions
{
    public static class DateTimeExtensions
    {
        public static bool Between(this DateTime datetime, DateTime lowerBound, DateTime upperBound)
        {
            if (lowerBound > upperBound)
            {
                string message = string.Format(
                    CultureInfo.CurrentCulture,
                    "{0} was later than the upper bound {1}",
                    lowerBound.ToString(CultureInfo.CurrentCulture),
                    upperBound.ToString(CultureInfo.CurrentCulture));

                throw new ArgumentException(message, nameof(datetime));
            }

            bool greaterThanLower = datetime >= lowerBound;
            bool lesserThanUpper = datetime <= upperBound;

            return greaterThanLower && lesserThanUpper;
        }
    }
}
