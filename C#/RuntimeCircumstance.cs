using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.RuntimeInformation;

namespace 
{
	public static class RuntimeCircumstance
	{
		private const string WindowsDirectory = @"C:\Program Files\dotnet";
		private const string LinuxDirectory = "/usr/share/dotnet";
		const string macOSX = "Mac OSX";
		private const string freeBSD = "FreeBSD";
		private const string unknown = "unknown platform";
		private const string dontKnowMessage = "I don't know what the natural dotnet install directory is on {0}";

		private static readonly string currentProcessDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) ?? string.Empty;

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
				throw new PlatformNotSupportedException(string.Format(dontKnowMessage, macOSX));
			}
			else if (IsOSPlatform(OSPlatform.FreeBSD))
			{
				throw new PlatformNotSupportedException(string.Format(dontKnowMessage, freeBSD));
			}
			else
			{
				throw new PlatformNotSupportedException(unknown);
			}
		}
	}
}
