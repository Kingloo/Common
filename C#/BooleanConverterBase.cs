﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace 
{
    public abstract class BooleanConverterBase<T> : DependencyObject, IValueConverter
    {
        public T True
        {
            get => (T)GetValue(TrueProperty);
            set => SetValue(TrueProperty, value);
        }

        public T False
        {
            get => (T)GetValue(FalseProperty);
            set => SetValue(FalseProperty, value);
        }

        public static readonly DependencyProperty TrueProperty = DependencyProperty.Register(
            "True",
            typeof(T),
            typeof(BooleanConverterBase<T>),
            new PropertyMetadata(default(T)));

        public static readonly DependencyProperty FalseProperty = DependencyProperty.Register(
            "False",
            typeof(T),
            typeof(BooleanConverterBase<T>),
            new PropertyMetadata(default(T)));

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => System.Convert.ToBoolean(value, culture) ? True : False;

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => default(T);
    }
}
