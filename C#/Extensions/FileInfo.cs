using System.Globalization;
using System.IO;

namespace .Extensions
{
    public static class FileInfoExtensions
    {
        public static string LengthHumanReadable(this FileInfo file)
            => LengthHumanReadable(file, CultureInfo.CurrentCulture);

        public static string LengthHumanReadable(this FileInfo file, CultureInfo ci)
        {
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
                _ when size < oneKiB => string.Format(ci, "{0} bytes", size),
                _ when size < oneMiB => string.Format(ci, "{0:0.##} KiB", size / oneKiB),
                _ when size < oneGiB => string.Format(ci, "{0:0.##} MiB", size / oneMiB),
                _ when size < oneTiB => string.Format(ci, "{0:0.##} GiB", size / oneGiB),
                _ when size < onePiB => string.Format(ci, "{0:0.##} TiB", size / oneTiB),
                _ when size < oneEiB => string.Format(ci, "{0:0.##} PiB", size / onePiB),
                _ when size < oneZiB => string.Format(ci, "{0:0.##} EiB", size / oneEiB),
                _ when size < oneYiB => string.Format(ci, "{0:0.##} ZiB", size / oneZiB),
                _ => string.Format(ci, "{0} YiB", size / oneYiB)
            };
        }
    }
}