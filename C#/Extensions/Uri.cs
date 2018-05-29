using System;
using System.Diagnostics;

namespace .Extensions
{
    public static class UriExtensions
    {
        public static void OpenInBrowser(this Uri uri)
        {
            if (uri == null) { throw new ArgumentNullException(nameof(uri)); }

            if (uri.IsAbsoluteUri)
            {
                Process.Start(uri.AbsoluteUri);
            }
        }
    }
}