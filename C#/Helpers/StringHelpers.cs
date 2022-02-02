using System;
using System.Runtime.CompilerServices;

namespace .Helpers
{
	internal static class StringHelpers
	{
		private const string http = "http";
		private const string https = "https";
		private const string colonSlashSlash = "://";

		internal static string EnsureStartsWithHttps(string value)
		{
			return EnsureStartsWithHttps(value.AsSpan()).ToString();
		}

		internal static ReadOnlySpan<char> EnsureStartsWithHttps(ReadOnlySpan<char> value)
		{
			if (value.StartsWith($"{https}{colonSlashSlash}"))
			{
				return value;
			}

			if (value.StartsWith($"{http}{colonSlashSlash}"))
			{
				string valueString = value.ToString();

				return valueString.Insert(4, "s").AsSpan();
			}

			if (value.Contains(colonSlashSlash, StringComparison.OrdinalIgnoreCase))
			{
				int indexOfAfterColonSlashSlash = value.IndexOf(colonSlashSlash) + 3;

				ReadOnlySpan<char> afterColonSlashSlash = value.Slice(indexOfAfterColonSlashSlash);

				return $"{https}{colonSlashSlash}{afterColonSlashSlash.ToString()}";
			}

			return $"{https}{colonSlashSlash}{value.ToString()}";
		}
	}
}
