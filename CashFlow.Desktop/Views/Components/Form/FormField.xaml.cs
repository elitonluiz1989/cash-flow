using CashFlow.Desktop.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CashFlow.Desktop.Views.Components.Form
{
    public partial class FormField : UserControl
    {
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(FormField), new PropertyMetadata("Field"));

        public EFormFieldType Type
        {
            get => (EFormFieldType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(EFormFieldType), typeof(FormField), new PropertyMetadata(EFormFieldType.Text));

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(object),
                typeof(FormField),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
            );

        public object ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(List<object>), typeof(FormField), new PropertyMetadata(null));

        public object FieldWidth
        {
            get => GetValue(FieldWidthProperty);
            set => SetValue(FieldWidthProperty, value);
        }

        public static readonly DependencyProperty FieldWidthProperty =
            DependencyProperty.Register(
                "FieldWidth",
                typeof(object),
                typeof(FormField),
                new PropertyMetadata(
                    defaultValue: null,
                    propertyChangedCallback: null,
                    coerceValueCallback: new CoerceValueCallback(FieldWidthCoerceValue)
                )
            );

        public EFormFieldFormattingType FormattingType
        {
            get => (EFormFieldFormattingType)GetValue(FormattingTypeProperty);
            set => SetValue(FormattingTypeProperty, value);
        }

        public static readonly DependencyProperty FormattingTypeProperty =
            DependencyProperty.Register(
                "FormattingType",
                typeof(EFormFieldFormattingType),
                typeof(FormField),
                new PropertyMetadata(EFormFieldFormattingType.None)
            );

        public ObservableCollection<ValidationError> ValidationErrors { get; set; } = new ObservableCollection<ValidationError>();

        private List<Key> _numericKeys;
        private List<Key> _directionalKeys;
        private List<Key> _controlKeys;
        private bool enableValueFormat = false;

        public FormField()
        {
            InitializeComponent();
            
            Validation.AddErrorHandler(this, ValidationErrorHandler);
            _numericKeys = new List<Key>()
            {
                Key.D0,
                Key.D1,
                Key.D2,
                Key.D3,
                Key.D4,
                Key.D5,
                Key.D6,
                Key.D7,
                Key.D8,
                Key.D9,
                Key.NumPad0,
                Key.NumPad1,
                Key.NumPad2,
                Key.NumPad3,
                Key.NumPad4,
                Key.NumPad5,
                Key.NumPad6,
                Key.NumPad7,
                Key.NumPad8,
                Key.NumPad9
            };

            _directionalKeys = new List<Key>() {
                Key.Left,
                Key.Up,
                Key.Right,
                Key.Down
            };

            _controlKeys = new List<Key>()
            {
                Key.Enter
            };
        }

        private static object? FieldWidthCoerceValue(DependencyObject obj, object value)
        {
            bool isParseable = double.TryParse(value.ToString(), out double parsedValue);

            if (!isParseable)
            {
                return null;
            }

            return parsedValue;
        }

        private void BindingValidationErrors(Control control)
        {
            if (ValidationErrors.Any())
            {
                BindingExpression? bindingExpression = null;

                if (control is TextBox tbValue)
                {
                    bindingExpression = tbValue.GetBindingExpression(TextBox.TextProperty);
                }

                if (control is ComboBox cbValue)
                {
                    bindingExpression = cbValue.GetBindingExpression(TextBox.TextProperty);
                }

                if (bindingExpression != null)
                {
                    Validation.MarkInvalid(bindingExpression, ValidationErrors?.FirstOrDefault());
                }
            }
        }

        private void FillListErrors(Control control)
        {
            ValidationErrors.Clear();

            foreach (ValidationError validationError in Validation.GetErrors(this).ToList())
            {
                ValidationErrors.Add(validationError);
            }

            BindingValidationErrors(control);
        }

        private void ValidationErrorHandler(object? sender, ValidationErrorEventArgs e)
        {
            ValidationErrors.Clear();
            ValidationErrors.Add(e.Error);

            Control control = this;

            if (TbValue.IsVisible)
            {
                control = TbValue;
            }

            if (CmbValue.IsVisible)
            {
                control = CmbValue;
            }

            BindingValidationErrors(control);
        }

        private void ValueToCurrency()
        {
            decimal convertedValue;
            string value = Regex.Replace(TbValue.Text, @"\.|,", "").TrimStart('0');

            if (value.Length < 3)
            {
                value = value.PadLeft(3, '0');
            }

            string firstPart = value[0..^2];
            string secondPart = value[^2..];

            value = string.Format("{0}.{1}", firstPart, secondPart);
            convertedValue = Convert.ToDecimal(value) / 100;

            Value = convertedValue.ToString("N2", CultureInfo.CurrentCulture);

            TbValue.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            TbValue.CaretIndex = TbValue.Text.Length;
        }

        private void TbValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FormattingType is EFormFieldFormattingType.Currency && !enableValueFormat)
            {
                ValueToCurrency();
            }

            FillListErrors((Control) sender);
        }

        private void CmbValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillListErrors((Control)sender);
        }

        private void TbValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (FormattingType is not EFormFieldFormattingType.None)
            {
                if (!_numericKeys.Contains(e.Key))
                {
                    e.Handled = true;
                }
            }
        }

        private void TbValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (FormattingType is not EFormFieldFormattingType.None)
            {
                if (_controlKeys.Contains(e.Key))
                {
                    enableValueFormat = false;
                    ValueToCurrency();
                }
                else if (_directionalKeys.Contains(e.Key))
                {
                    enableValueFormat = true;
                }
            }
        }
    }
}
