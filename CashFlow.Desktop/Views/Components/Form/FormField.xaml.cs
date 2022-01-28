using CashFlow.Desktop.Enums;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
            DependencyProperty.Register("Label", typeof(string), typeof(FormField), new PropertyMetadata(default(string)));

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
            DependencyProperty.Register("Value", typeof(object), typeof(FormField), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

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
                    coerceValueCallback: new CoerceValueCallback(FieldWidth_CoerceValue)
                )
            );

        public FormField()
        {
            InitializeComponent();
        }

        private static object? FieldWidth_CoerceValue(DependencyObject obj, object value)
        {
            bool isParseable = double.TryParse(value.ToString(), out double parsedValue);

            if (!isParseable)
            {
                return null;
            }

            return parsedValue;
        }
    }
}
