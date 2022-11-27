using System.Globalization;

namespace .Common
{
	internal static class BytesToHumanReadableConverter
	{
		private const decimal oneKiB = 1024m;
		private const decimal oneMiB = oneKiB * oneKiB;
		private const decimal oneGiB = oneKiB * oneMiB;
		private const decimal oneTiB = oneKiB * oneGiB;
		private const decimal onePiB = oneKiB * oneTiB;
		private const decimal oneEiB = oneKiB * onePiB;
		private const decimal oneZiB = oneKiB * oneEiB;
		private const decimal oneYiB = oneKiB * oneZiB;
		private const decimal oneRiB = oneKiB * oneYiB;
		
		// private const decimal oneQiB = oneKiB * oneRiB;
		// larger than decimal.MaxValue !!

		internal static string Format(long bytes)
			=> Format(bytes, CultureInfo.CurrentCulture);

		internal static string Format(long bytes, CultureInfo cultureInfo)
		{
			decimal decimalBytes = System.Convert.ToDecimal(bytes);

			return bytes switch
			{
				_ when bytes < oneKiB => FormatNumber(decimalBytes, "bytes", 0, cultureInfo),
				_ when bytes < oneMiB => FormatNumber(decimalBytes / oneKiB, "KiB", 2, cultureInfo),
				_ when bytes < oneGiB => FormatNumber(decimalBytes / oneMiB, "MiB", 2, cultureInfo),
				_ when bytes < oneTiB => FormatNumber(decimalBytes / oneGiB, "GiB", 2, cultureInfo),
				_ when bytes < onePiB => FormatNumber(decimalBytes / oneTiB, "TiB", 2, cultureInfo),
				_ when bytes < oneEiB => FormatNumber(decimalBytes / onePiB, "PiB", 3, cultureInfo),
				_ when bytes < oneZiB => FormatNumber(decimalBytes / oneEiB, "EiB", 3, cultureInfo),
				_ when bytes < oneYiB => FormatNumber(decimalBytes / oneZiB, "ZiB", 3, cultureInfo),
				_ when bytes < oneRiB => FormatNumber(decimalBytes / oneYiB, "YiB", 3, cultureInfo),
				_ => FormatNumber(decimalBytes / oneRiB, "RiB", 3, cultureInfo)
			};
		}

		private static string FormatNumber(decimal number, string unit, uint decimalPlaces, CultureInfo cultureInfo)
		{
			return string.Format(
				cultureInfo,
				"{0} {1}",
				number.ToString($"N{decimalPlaces}", cultureInfo),
				unit);
		}
	}
}
