using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Extensions.Hosting
{
    public static class SystemHostServiceExtensions
    {
        public class SystemHostServiceException : Exception
        {
            public SystemHostServiceException() { }

            public SystemHostServiceException(string? message)
                : base(message)
            { }

            public SystemHostServiceException(string? message, Exception? innerException)
                : base(message, innerException)
            { }

            protected SystemHostServiceException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            { }
        }

        public static IHostBuilder ConfigureSystemHostService(this IHostBuilder hostBuilder, string[] args)
        {
            if (IsSystemd(args) && IsWindowsService(args))
            {
                throw new SystemHostServiceException("cannot specify both SystemD and Windows Service");
            }
            
            if (IsSystemd(args))
            {
                return hostBuilder.UseSystemd();
            }
            
            if (IsWindowsService(args))
            {
                return hostBuilder.UseWindowsService();
            }
            
            return hostBuilder;
        }

        private static bool IsSystemd(string[] args)
        {
            return ContainsArg(args, "--systemd");
        }

        private static bool IsWindowsService(string[] args)
        {
            return ContainsArg(args, "--windowsservice");
        }

        private static bool ContainsArg(string[] args, string arg)
        {
            return args.Any(a => a.ToLower().Equals(arg, StringComparison.Ordinal));
        }
    }
}
