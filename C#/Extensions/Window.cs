using System;
using System.Globalization;
using System.Windows;

namespace .Extensions
{
    public static class WindowExtensions
    {
        public static void MoveToMiddleOfScreen(this Window window, double screenWidth, double screenHeight)
        {
            if (window == null) { throw new ArgumentNullException(nameof(window)); }
            
            if (screenWidth < 1)
            {
                string message = string.Format(
                    CultureInfo.CurrentCulture,
                    "screenWidth was {0}, it cannot be < 1",
                    screenWidth);

                throw new ArgumentException(message, nameof(screenWidth));
            }

            if (screenHeight < 1)
            {
                string message = string.Format(
                    CultureInfo.CurrentCulture,
                    "screenHeight was {0}, it cannot be < 1",
                    screenHeight);

                throw new ArgumentException(message, nameof(screenHeight));
            }

            double left = (screenWidth / 2d) - (window.Width / 2d);
            double top = (screenHeight / 2d) - (window.Height / 2d);

            window.Left = Math.Max(left, 0);
            window.Top = Math.Max(top, 0);
        }
    }
}
