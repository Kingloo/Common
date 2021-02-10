using System;
using System.Runtime.Serialization;

namespace .Common
{
    public class TemperatureBelowAbsoluteZeroException : Exception
    {
        public double Value { get; set; } = default(double);

        public TemperatureBelowAbsoluteZeroException(double value)
            : this($"value was below absolute zero ({value})")
        {
            Value = value;
        }

        public TemperatureBelowAbsoluteZeroException(string message)
            : base(message)
        { }

        public TemperatureBelowAbsoluteZeroException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected TemperatureBelowAbsoluteZeroException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}