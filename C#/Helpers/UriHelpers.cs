using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace .Helpers
{
    public static class UriHelpers
    {
        /// <summary>
        /// Removes a path segment that matches a Regex pattern.
        /// </summary>
        /// <param name="uri">The Uri to remove the segment from.</param>
        /// <param name="pattern">Regex pattern to match against.</param>
        /// <returns>A new Uri with amended path, but otherwise identical.</returns>
        public static Uri RemovePathSegment(Uri uri, string pattern)
        {
            if (String.IsNullOrEmpty(pattern))
            {
                throw new ArgumentException("invalid Regex pattern", nameof(pattern));
            }

            var segmentsToKeep = uri.Segments.Where(x => !Regex.IsMatch(x, pattern));

            StringBuilder path = new StringBuilder();

            foreach (string each in segmentsToKeep)
            {
                path.Append(each);
            }

            return new UriBuilder
            {
                Fragment = uri.Fragment,
                Host = uri.Host,
                Path = path.ToString(),
                Port = uri.Port,
                Query = uri.Query,
                Scheme = uri.Scheme
            }
            .Uri;
        }

        /// <summary>
        /// Removes the query from a Uri.
        /// </summary>
        /// <param name="uri">The Uri to remove the query from.</param>
        /// <returns>An otherwise identical Uri.</returns>
        public static Uri RemoveQuery(Uri uri)
        {
            return new UriBuilder
            {
                Fragment = uri.Fragment,
                Host = uri.Host,
                Path = uri.AbsolutePath,
                Port = uri.Port,
                Query = string.Empty,
                Scheme = uri.Scheme
            }
            .Uri;
        }
    }
}
