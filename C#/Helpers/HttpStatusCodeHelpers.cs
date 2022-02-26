using System;
using System.Net;

namespace .Helpers
{
	public static class HttpStatusCodeHelpers
	{
		public static string FormatStatusCode(HttpStatusCode? statusCode)
		{
			if (statusCode is null)
			{
				throw new ArgumentNullException(nameof(statusCode));
			}

			return $"{statusCode} ({(int)statusCode})";
		}
	}
}
