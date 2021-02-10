namespace .Common
{
    public static class TemperatureHelpers
    {
        public const double AbsoluteZeroKelvin = 0.0d;
        public const double AbsoluteZeroCelsius = -273.15d;

        public static Celsius FromKelvin(Kelvin kelvin)
        {
            return new Celsius(kelvin.Value + AbsoluteZeroCelsius);
        }

        public static Kelvin FromCelsius(Celsius celsius)
        {
            return new Kelvin(celsius.Value - AbsoluteZeroCelsius);
        }

        public static bool AreEqual(Celsius celsius, Kelvin kelvin)
        {
            return celsius == TemperatureHelpers.FromKelvin(kelvin);
        }
    }
}