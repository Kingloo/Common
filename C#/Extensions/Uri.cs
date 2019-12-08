using System;
using System.Diagnostics;

namespace .Extensions
{
    public static class UriExtensions
    {
        public static void OpenInBrowser(this Uri uri)
        {
            if (uri.IsAbsoluteUri)
            {
                ProcessStartInfo pInfo = new ProcessStartInfo(uri.AbsoluteUri)
                {
                    UseShellExecute = true
                };

                Process.Start(pInfo);
            }
        }
    }
}