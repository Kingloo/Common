using System;
using System.Diagnostics.CodeAnalysis;

namespace .Common
{
    public class Kelvin : Primitive<double, Kelvin>
    {
        public Kelvin()
            : this(0.0d)
        { }

        public Kelvin(double temperature)
            : base(temperature)
        { }

        public static Kelvin AbsoluteZero()
        {
            return Kelvin.From(TemperatureHelpers.AbsoluteZeroKelvin);
        }

        public override int CompareTo([AllowNull] Primitive<double, Kelvin> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Value.CompareTo(other.Value);
        }

        protected override void Validate(double value)
        {
            if (value < TemperatureHelpers.AbsoluteZeroKelvin)
            {
                throw new TemperatureBelowAbsoluteZeroException(value);
            }
        }
    }
}