﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace 
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
            if (uri is null) { throw new ArgumentNullException(nameof(uri)); }

            if (String.IsNullOrEmpty(pattern))
            {
                throw new ArgumentException("invalid Regex pattern", nameof(pattern));
            }

            List<string> segmentsToKeep = uri.Segments
                .Where(x => !Regex.IsMatch(x, pattern))
                .ToList();
            
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
            if (uri is null) { throw new ArgumentNullException(nameof(uri)); }

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