using System.Globalization;
using System.IO;

namespace .Extensions
{
    public static class FileInfoExtensions
    {
        public static string LengthHumanReadable(this FileInfo file)
        {
            var cc = CultureInfo.CurrentCulture;

            decimal oneKiB = 1024m;
            decimal oneMiB = oneKiB * oneKiB;
            decimal oneGiB = oneKiB * oneMiB;
            decimal oneTiB = oneKiB * oneGiB;
            decimal onePiB = oneKiB * oneTiB;
            decimal oneEiB = oneKiB * onePiB;
            decimal oneZiB = oneKiB * oneEiB;
            decimal oneYiB = oneKiB * oneZiB;

            decimal size = System.Convert.ToDecimal(file.Length);

            return size switch
            {
                _ when size < oneKiB => string.Format(cc, "{0} bytes", size),
                _ when size < oneMiB => string.Format(cc, "{0:0.##} KiB", size / oneKiB),
                _ when size < oneGiB => string.Format(cc, "{0:0.##} MiB", size / oneMiB),
                _ when size < oneTiB => string.Format(cc, "{0:0.##} GiB", size / oneGiB),
                _ when size < onePiB => string.Format(cc, "{0:0.##} TiB", size / oneTiB),
                _ when size < oneEiB => string.Format(cc, "{0:0.##} PiB", size / onePiB),
                _ when size < oneZiB => string.Format(cc, "{0:0.##} EiB", size / oneEiB),
                _ when size < oneYiB => string.Format(cc, "{0:0.##} ZiB", size / oneZiB),
                _ => string.Format(cc, "{0} YiB", size / oneYiB)
            };
        }
    }
}