using System;

namespace .Extensions
{
	public static class UriExtensions
	{
		public static string RemoveQuery(this Uri uri)
		{
			if (uri is null) { throw new ArgumentNullException(nameof(uri)); }

			const string questionMark = "?";

			if (!uri.AbsoluteUri.Contains(questionMark))
			{
				return uri.AbsoluteUri;
			}

			return uri.AbsoluteUri.Split(questionMark, StringSplitOptions.RemoveEmptyEntries)[0];
		}
	}
}
