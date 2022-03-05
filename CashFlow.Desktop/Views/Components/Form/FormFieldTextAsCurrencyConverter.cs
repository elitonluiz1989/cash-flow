using CashFlow.Desktop.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CashFlow.Desktop.Views.Components.Form
{
    internal class FormFieldTextAsCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCurrencyFormat = (EFormFieldFormattingType)value is EFormFieldFormattingType.Currency;
            return isCurrencyFormat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).ToString(culture);
        }
    }
}
