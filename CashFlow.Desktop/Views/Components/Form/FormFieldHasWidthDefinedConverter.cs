using System;
using System.Globalization;
using System.Windows.Data;

namespace CashFlow.Desktop.Views.Components.Form
{
    internal class FormFieldHasWidthDefinedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool widthDefined = value is not null;

            if (widthDefined)
            {
                widthDefined = double.TryParse(value?.ToString(), out double parsedValue);
            }

            return widthDefined;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
