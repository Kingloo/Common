using System.Linq;

namespace .Common
{
    public static class UserAgents
    {
        public const string Firefox83 = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:83.0) Gecko/20100101 Firefox/83.0";
        public const string MsEdge87 = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.66 Safari/537.36 Edg/87.0.664.41";
        public const string MacOSXSafari13_1 = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_4) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.1 Safari/605.1.15";
        public const string GoogleChrome85 = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36";
        public const string Opera66 = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36 OPR/66.0.3515.72";

        public static string GetRandomUserAgent()
        {
            // .TickCount, measured in milliseconds, increments so quickly that the last digit is random enough for our needs
            
            return System.Environment.TickCount.ToString().Last().ToString() switch
            {
                "1" => Firefox83,
                "2" => MsEdge87,
                "3" => MacOSXSafari13_1,
                "4" => GoogleChrome85,
                _ => Opera66
            };
        }
    }
}