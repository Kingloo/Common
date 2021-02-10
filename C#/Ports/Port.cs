using System;
using System.Diagnostics.CodeAnalysis;

namespace .Common
{
    public class Port : Primitive<int, Port>
    {
        public bool AllowAdminPorts { get; set; } = false;
        
        public Port()
            : this(0, false)
        { }

        public Port(int port)
            : this(port, false)
        { }

        public Port(int port, bool allowAdminPorts)
            : base(port)
        {
            Value = port;
            AllowAdminPorts = allowAdminPorts;
        }

        public static Port From(int value, bool allowAdminPorts)
        {
            Port port = new Port(value, allowAdminPorts);
            
            port.Validate(value);
            
            return port;
        }

        public override int CompareTo([AllowNull] Primitive<int, Port> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Value.CompareTo(other.Value);
        }

        protected override void Validate(int value)
        {
            int minimumAllowedPort = AllowAdminPorts ? 1 : 1024;
            int maximumPort = 65535;

            if (value < minimumAllowedPort || value > maximumPort)
            {
                throw new InvalidPortException(value, AllowAdminPorts);
            }
        }
    }
}