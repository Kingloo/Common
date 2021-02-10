using System;
using System.Runtime.Serialization;

namespace .Common
{
    public class InvalidPortException : Exception
    {
        private const int minimumAdminPort = 1;
        private const int minimumPort = 1024;
        private const int maximumPort = 65535;

        public int Port { get; private set; } = 0;

        public InvalidPortException(int port, bool areAdminPortsAllowed)
            : this(GetErrorMessage(port, areAdminPortsAllowed))
        {
            Port = port;
        }

        public InvalidPortException(string message)
            : base(message)
        { }

        public InvalidPortException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected InvalidPortException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        private static string GetErrorMessage(int port, bool areAdminPortsAllowed)
        {
            if (port > maximumPort)
            {
                return $"port exceeds maximum (yours: {port}, maximum port: {maximumPort})";
            }

            if (!areAdminPortsAllowed && port < minimumPort)
            {
                return $"port is less than minimum allowed non-admin port (yours: {port}, minimum (non-admin): {minimumPort})";
            }

            if (areAdminPortsAllowed && port < minimumAdminPort)
            {
                return $"port is less than minimum allowed admin port (yours: {port}, minimum: {minimumAdminPort})";
            }

            return $"unknown error";
        }
    }
}