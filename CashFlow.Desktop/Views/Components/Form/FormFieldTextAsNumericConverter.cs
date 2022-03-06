using CashFlow.Desktop.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CashFlow.Desktop.Views.Components.Form
{
    internal class FormFieldTextAsNumericConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isNumericFormat = (EFormFieldFormattingType)value is EFormFieldFormattingType.Numeric;
            return isNumericFormat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).ToString(culture);
        }
    }
}
