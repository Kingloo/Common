using System.Net;

namespace .Helpers
{
	public static class HttpStatusCodeHelpers
	{
		public static string FormatStatusCode(HttpStatusCode? statusCode)
		{
			if (statusCode is not null)
			{
				return $"{statusCode} ({(int)statusCode})";
			}
			else
			{
				return "unknown";
			}
		}
	}
}
