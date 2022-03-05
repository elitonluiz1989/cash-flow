using System;
using System.Globalization;
using System.Windows.Data;

namespace CashFlow.Desktop.Views.Components.Form
{
    internal class FormFieldHasMaxLengthDefinedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool maxLengthDefined = value is not null;

            if (maxLengthDefined)
            {
                maxLengthDefined = double.TryParse(value?.ToString(), out double parsedValue);
            }

            return maxLengthDefined;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).ToString(culture);
        }
    }
}
