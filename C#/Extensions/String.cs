using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace .Extensions
{
	public static class StringExtensions
	{
		private const string https = "https://";
		private const string http = "http://";

		public static bool ContainsExt(this string target, string toFind, StringComparison comparison)
		{
			if (String.IsNullOrWhiteSpace(target))
			{
				throw new ArgumentNullException(nameof(target));
			}

			if (String.IsNullOrWhiteSpace(toFind))
			{
				throw new ArgumentNullException(nameof(toFind));
			}

			return (target.IndexOf(toFind, comparison) > -1);
		}

		public static string RemoveNewLines(this string value)
		{
			var sco = StringComparison.Ordinal;

			string toReturn = value;

			if (toReturn.ContainsExt("\r\n", sco))
			{
				toReturn = toReturn.Replace("\r\n", " ", StringComparison.OrdinalIgnoreCase);
			}

			if (toReturn.ContainsExt("\r", sco))
			{
				toReturn = toReturn.Replace("\r", " ", StringComparison.OrdinalIgnoreCase);
			}

			if (toReturn.ContainsExt("\n", sco))
			{
				toReturn = toReturn.Replace("\n", " ", StringComparison.OrdinalIgnoreCase);
			}

			if (toReturn.ContainsExt(Environment.NewLine, sco))
			{
				toReturn = toReturn.Replace(Environment.NewLine, " ", StringComparison.OrdinalIgnoreCase);
			}

			return toReturn;
		}

		public static string RemoveUnicodeCategories(this string self, IEnumerable<UnicodeCategory> categories)
		{
			if (String.IsNullOrWhiteSpace(self))
			{
				throw new ArgumentNullException(nameof(self));
			}

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

		public static IReadOnlyCollection<string> FindBetween(this string text, string beginning, string ending)
		{
			if (String.IsNullOrEmpty(beginning))
			{
				throw new ArgumentException("beginning was NullOrEmpty", nameof(beginning));
			}

			if (String.IsNullOrEmpty(ending))
			{
				throw new ArgumentException("ending was NullOrEmpty", nameof(ending));
			}

			List<string> results = new List<string>();

			string pattern = string.Format(
				CultureInfo.CurrentCulture,
				"{0}({1}){2}",
				Regex.Escape(beginning),
				".+?",
				Regex.Escape(ending));

			foreach (Match? m in Regex.Matches(text, pattern))
			{
				if (m is not null)
				{
					results.Add(m.Groups[1].Value);
				}
			}

			return results;
		}

		public static string EnsureStartsWithHttps(this string input)
		{
			if (String.IsNullOrWhiteSpace(input))
			{
				throw new ArgumentNullException(nameof(input));
			}

			if (input.StartsWith(https, StringComparison.OrdinalIgnoreCase))
			{
				return input;
			}

			if (input.StartsWith(http, StringComparison.OrdinalIgnoreCase))
			{
				return input.Insert(4, "s");
			}

			return string.Format(CultureInfo.CurrentCulture, "{0}{1}", https, input);
		}
	}
}
