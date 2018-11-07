﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace .Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsExt(this string target, string toFind, StringComparison comparison)
        {
            if (target is null) { throw new ArgumentNullException(nameof(target)); }

            return (target.IndexOf(toFind, comparison) > -1);
        }


        public static string RemoveNewLines(this string value)
        {
            if (value is null) { throw new ArgumentNullException(nameof(value)); }

            string toReturn = value;

            if (toReturn.Contains("\r\n"))
            {
                toReturn = toReturn.Replace("\r\n", " ");
            }

            if (toReturn.Contains("\r"))
            {
                toReturn = toReturn.Replace("\r", " ");
            }

            if (toReturn.Contains("\n"))
            {
                toReturn = toReturn.Replace("\n", " ");
            }

            if (toReturn.Contains(Environment.NewLine))
            {
                toReturn = toReturn.Replace(Environment.NewLine, " ");
            }

            return toReturn;
        }

        
        public static string RemoveUnicodeCategories(this string self, IEnumerable<UnicodeCategory> categories)
        {
            if (self is null) { throw new ArgumentNullException(nameof(self)); }
            if (categories is null) { throw new ArgumentNullException(nameof(categories)); }

            var sb = new StringBuilder();

            foreach (char c in self)
            {
                if (!categories.Any(category => category == Char.GetUnicodeCategory(c)))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }


        public static IReadOnlyList<string> FindBetween(this string text, string beginning, string ending)
        {
            if (text is null) { throw new ArgumentNullException(nameof(text)); }

            if (String.IsNullOrEmpty(beginning)) { throw new ArgumentException("beginning was NullOrEmpty", nameof(beginning)); }
            if (String.IsNullOrEmpty(ending)) { throw new ArgumentException("ending was NullOrEmpty", nameof(ending)); }

            List<string> results = new List<string>();

            string pattern = string.Format(
                CultureInfo.CurrentCulture,
                "{0}({1}){2}",
                Regex.Escape(beginning),
                ".+?",
                Regex.Escape(ending));

            foreach (Match m in Regex.Matches(text, pattern))
            {
                results.Add(m.Groups[1].Value);
            }

            return results;
        }
    }
}
