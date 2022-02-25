using System;

namespace .Extensions
{
	public static class UriExtensions
	{
		private const char questionMark = '?';

		public static string RemoveQuery(this Uri uri)
		{
			if (uri is null)
			{
				throw new ArgumentNullException(nameof(uri));
			}

			string absoluteUri = uri.AbsoluteUri;

			return absoluteUri.Contains(questionMark)
				? absoluteUri.Split(questionMark, StringSplitOptions.RemoveEmptyEntries)[0]
				: absoluteUri;
		}
	}
}
