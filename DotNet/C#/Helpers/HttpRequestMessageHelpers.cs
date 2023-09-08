using System.Net;
using System.Text;

namespace .Helpers
{
	internal static class HttpRequestMessageHelpers
	{
		private const char semiColon = ';';

		internal static string FormatCookieIntoHeaderValue(Cookie cookie)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append($"{cookie.Name}={cookie.Value}");
			sb.Append(semiColon);

			sb.Append($"Domain={cookie.Domain}");
			sb.Append(semiColon);

			sb.Append($"Expires={cookie.Expires}");
			sb.Append(semiColon);

			sb.Append($"Path={cookie.Path}");

			if (cookie.Secure)
			{
				sb.Append(semiColon);

				sb.Append($"Secure");
			}

			return sb.ToString();
		}
	}
}
