using System;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.RuntimeInformation;

namespace 
{
	public static class RuntimeCircumstance
	{
		private const string WindowsDirectory = @"C:\Program Files\dotnet";
		private const string LinuxDirectory = "/usr/share/dotnet";

		private static readonly string currentProcessDirectory = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) ?? string.Empty;

		public static string GetRealLocation()
		{
			return IsRunByDotnet() switch
			{
				true => AppContext.BaseDirectory, // `dotnet .\path\to\lib.dll`
				false => currentProcessDirectory // `.\path\to\executable.exe`
			};
		}

		public static bool IsRunByDotnet()
		{
			string dontKnowMessage = "I don't know what the natural dotnet install directory is on {0}";

			if (IsOSPlatform(OSPlatform.Windows))
			{
				return currentProcessDirectory.Equals(WindowsDirectory, StringComparison.Ordinal);
			}
			else if (IsOSPlatform(OSPlatform.Linux))
			{
				return currentProcessDirectory.Equals(LinuxDirectory, StringComparison.OrdinalIgnoreCase);
			}
			else if (IsOSPlatform(OSPlatform.OSX))
			{
				throw new PlatformNotSupportedException(string.Format(dontKnowMessage, "Mac OSX"));
			}
			else if (IsOSPlatform(OSPlatform.FreeBSD))
			{
				throw new PlatformNotSupportedException(string.Format(dontKnowMessage, "FreeBSD"));
			}
			else
			{
				throw new PlatformNotSupportedException("unknown platform");
			}
		}
	}
}
