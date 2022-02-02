using System.Net;

namespace .Helpers
{
	public static class HttpStatusCodeHelpers
	{
		public static string FormatStatusCode(HttpStatusCode statusCode)
		{
			return $"{statusCode} ({(int)statusCode})";
		}
	}
}
