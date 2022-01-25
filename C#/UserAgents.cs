using System;
using System.Collections.Generic;
using System.Linq;

namespace .Common
{
	public static class UserAgents
	{
		public const string HeaderName = "User-Agent";

		public const string Firefox_96_Windows = nameof(Firefox_96_Windows);
		public const string Firefox_91_ESR_Linux = nameof(Firefox_91_ESR_Linux);
		public const string Edge_97_Windows = nameof(Edge_97_Windows);
		public const string Edge_99_Linux = nameof(Edge_99_Linux);
		public const string Safari_13_1_MacOSX = nameof(Safari_13_1_MacOSX);
		public const string Chrome_85_Windows = nameof(Chrome_85_Windows);
		public const string Opera_66_Windows = nameof(Opera_66_Windows);

		private static readonly IDictionary<string, string> agents = new Dictionary<string, string>
		{
			{ Firefox_96_Windows, "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0" },
			{ Firefox_91_ESR_Linux, "Mozilla/5.0 (X11; Linux x86_64; rv:91.0) Gecko/20100101 Firefox/91.0" },
			{ Edge_97_Windows, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36 Edg/97.0.1072.69" },
			{ Edge_99_Linux, "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4814.0 Safari/537.36 Edg/99.0.1135.6" },
			{ Safari_13_1_MacOSX, "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_4) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.1 Safari/605.1.15" },
			{ Chrome_85_Windows, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36" },
			{ Opera_66_Windows, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36 OPR/66.0.3515.72" }
		};

		public static string Get(string browser)
		{
			return agents[browser];
		}

		public static string GetRandomUserAgent()
		{
			Random random = new Random();

			int randomNumber = random.Next(0, agents.Count - 1);

			return agents.Values.ToArray()[randomNumber];
		}
	}
}
