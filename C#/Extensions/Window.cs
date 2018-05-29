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
                throw new ArgumentOutOfRangeException(nameof(screenWidth));
            }

            if (screenHeight < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(screenHeight));
            }

            double left = (screenWidth / 2d) - (window.Width / 2d);
            double top = (screenHeight / 2d) - (window.Height / 2d);

            window.Left = Math.Max(left, 0d);
            window.Top = Math.Max(top, 0d);
        }
    }
}
