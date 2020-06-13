using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace .Common
{
    public static class JsonHelpers
    {
        public static bool TryParse(string input, out JObject json)
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
