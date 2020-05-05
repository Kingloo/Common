﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace .Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanNegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;
    }
}