using System;
using System.Runtime.InteropServices;

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
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return currentProcessDirectory.Equals(WindowsDirectory, StringComparison.Ordinal);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return currentProcessDirectory.Equals(LinuxDirectory, StringComparison.OrdinalIgnoreCase);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                throw new PlatformNotSupportedException("I don't know what the natural dotnet install directory is on Mac OSX");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                throw new PlatformNotSupportedException("I don't know what the natural dotnet install directory is on FreeBSD");
            }
            else
            {
                throw new PlatformNotSupportedException("unknown platform");
            }
        }
    }
}
