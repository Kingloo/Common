using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace .Helpers
{
	internal static class JsonHelpers
	{
		internal static bool TryParse(string input, [NotNullWhen(true)] out JObject? json)
		{
			try
			{
				json = JObject.Parse(input);

				return true;
			}
			catch (JsonReaderException)
			{
				json = null;

				return false;
			}
		}
	}
}
