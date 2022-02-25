using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace 
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

	public static class SystemHostServiceExtensions
	{
		public static IHostBuilder ConfigureSystemHostService(this IHostBuilder hostBuilder, string[] args)
		{
			return ConfigureSystemHostService(hostBuilder, args, addSystemdConsoleLogger: true);
		}

		public static IHostBuilder ConfigureSystemHostService(this IHostBuilder hostBuilder, string[] args, bool addSystemdConsoleLogger)
		{
			if (hostBuilder is null)
			{
				throw new ArgumentNullException(nameof(hostBuilder));
			}

			if (IsSystemd(args) && IsWindowsService(args))
			{
				throw new SystemHostServiceException("cannot specify both SystemD and Windows Service");
			}

			if (IsSystemd(args))
			{
				if (addSystemdConsoleLogger)
				{
					hostBuilder.ConfigureServices((ctx, services) =>
					{
						services.AddLogging(logging =>
						{
							logging.AddSystemdConsole();
						});
					});
				}

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
			return args.Any(a => a.Equals(arg, StringComparison.OrdinalIgnoreCase));
		}
	}
}
