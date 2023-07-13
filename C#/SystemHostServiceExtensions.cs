using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Systemd;
using Microsoft.Extensions.Hosting.WindowsServices;

namespace .Common
{
	public static class SystemHostServiceExtensions
	{
		public static IHostBuilder ConfigureSystemHostService(this IHostBuilder hostBuilder)
		{
			ArgumentNullException.ThrowIfNull(hostBuilder);

			if (SystemdHelpers.IsSystemdService())
			{
				return hostBuilder.UseSystemd();
			}

			if (WindowsServiceHelpers.IsWindowsService())
			{
				return hostBuilder.UseWindowsService();
			}

			return hostBuilder;
		}
	}
}
