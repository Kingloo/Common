using System;
using System.Diagnostics.CodeAnalysis;

namespace .Common
{
    public class Celsius : Primitive<double, Celsius>
    {
        public Celsius()
            : this(0.0d)
        { }

        public Celsius(double temperature)
            : base(temperature)
        { }

        public static Celsius AbsoluteZero()
        {
            return Celsius.From(TemperatureHelpers.AbsoluteZeroCelsius);
        }

        public override int CompareTo([AllowNull] Primitive<double, Celsius> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Value.CompareTo(other.Value);
        }

        protected override void Validate(double value)
        {
            if (value < TemperatureHelpers.AbsoluteZeroCelsius)
            {
                throw new TemperatureBelowAbsoluteZeroException(value);
            }
        }
    }
}