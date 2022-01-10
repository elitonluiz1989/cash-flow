using CashFlow.Desktop.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CashFlow.Desktop.Views.Components.Form
{
    internal class FormFieldIsComboboxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (EFormFieldType)value is EFormFieldType.Combobox;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
