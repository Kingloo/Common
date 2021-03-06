﻿using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace .Common
{
    public static class JsonHelpers
    {
        [System.Diagnostics.DebuggerStepThrough]
        public static bool TryParse(string input, [NotNullWhen(true)] out JObject? json)
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
