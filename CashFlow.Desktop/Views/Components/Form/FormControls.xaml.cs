using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CashFlow.Desktop.Views.Components.Form
{
    public partial class FormControls : UserControl
    {
        public string CancelContent
        {
            get => (string)GetValue(CancelContentProperty);
            set => SetValue(CancelContentProperty, value);
        }

        public static readonly DependencyProperty CancelContentProperty =
            DependencyProperty.Register(
                "CancelContent",
                typeof(string),
                typeof(FormControls),
                new PropertyMetadata("Cancel")
            );

        public ICommand CancelCommand
        {
            get => (ICommand)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register(
                "CancelCommand",
                typeof(ICommand),
                typeof(FormControls),
                new PropertyMetadata(null, new PropertyChangedCallback(CommandChanged))
            );

        public object CancelCommandParameter
        {
            get => GetValue(CancelCommandParameterProperty);
            set => SetValue(CancelCommandParameterProperty, value);
        }

        public static readonly DependencyProperty CancelCommandParameterProperty =
            DependencyProperty.Register(
                "CancelCommandParameter",
                typeof(object),
                typeof(FormControls),
                new PropertyMetadata(default(object))
            );

        public string ResetContent
        {
            get => (string)GetValue(ResetContentProperty);
            set => SetValue(ResetContentProperty, value);
        }

        public static readonly DependencyProperty ResetContentProperty =
            DependencyProperty.Register(
                "RestContent",
                typeof(string),
                typeof(FormControls),
                new PropertyMetadata("Reset")
            );

        public ICommand ResetCommand
        {
            get => (ICommand)GetValue(ResetCommandProperty);
            set => SetValue(ResetCommandProperty, value);
        }

        public static readonly DependencyProperty ResetCommandProperty =
            DependencyProperty.Register(
                "ResetCommand",
                typeof(ICommand),
                typeof(FormControls),
                new PropertyMetadata(null, new PropertyChangedCallback(CommandChanged))
            );

        public object ResetCommandParameter
        {
            get => GetValue(ResetCommandParameterProperty);
            set => SetValue(ResetCommandParameterProperty, value);
        }

        public static readonly DependencyProperty ResetCommandParameterProperty =
            DependencyProperty.Register(
                "ResetCommandParameter",
                typeof(object),
                typeof(FormControls),
                new PropertyMetadata(default(object))
            );
        public string SubmitContent
        {
            get => (string)GetValue(SubmitContentProperty);
            set => SetValue(SubmitContentProperty, value);
        }

        public static readonly DependencyProperty SubmitContentProperty =
            DependencyProperty.Register(
                "SubmitContent",
                typeof(string),
                typeof(FormControls),
                new PropertyMetadata("Submit")
            );

        public ICommand SubmitCommand
        {
            get => (ICommand)GetValue(SubmitCommandProperty);
            set => SetValue(SubmitCommandProperty, value);
        }

        public static readonly DependencyProperty SubmitCommandProperty =
            DependencyProperty.Register(
                "SubmitCommand",
                typeof(ICommand),
                typeof(FormControls),
                new PropertyMetadata(null, new PropertyChangedCallback(CommandChanged))
            );

        public object SubmitCommandParameter
        {
            get => GetValue(SubmitCommandParameterProperty);
            set => SetValue(SubmitCommandParameterProperty, value);
        }

        public static readonly DependencyProperty SubmitCommandParameterProperty =
            DependencyProperty.Register(
                "SubmitCommandParameter",
                typeof(object),
                typeof(FormControls),
                new PropertyMetadata(default(object))
            );

        public FormControls()
        {
            InitializeComponent();
        }

        private static bool VerifyCanExecute(ICommand command, object? commandParameter)
        {
            bool isEnabled = (command is RoutedCommand cmd) ? 
                cmd.CanExecute(commandParameter, null) :
                command.CanExecute(commandParameter);

            return isEnabled;
        }

        private void CanExecuteChanged(object? sender, EventArgs e)
        {
            if (CancelCommand != null)
            {
                ButtonCancel.IsEnabled = VerifyCanExecute(CancelCommand, CancelCommandParameter);
            }

            if (ResetCommand != null)
            {
                ButtonReset.IsEnabled = VerifyCanExecute(ResetCommand, ResetCommandParameter);
            }

            if (SubmitCommand != null)
            {
                ButtonSubmit.IsEnabled = VerifyCanExecute(SubmitCommand, SubmitCommandParameter);
            }
        }

        private void RemoveCommand(ICommand command)
        {
            EventHandler handler = CanExecuteChanged;
            command.CanExecuteChanged -= handler;
        }

        private void AddCommand(ICommand newCommand)
        {
            EventHandler handler = new(CanExecuteChanged);
            newCommand.CanExecuteChanged += handler;
        }

        private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
        {
            if (oldCommand != null)
            {
                RemoveCommand(oldCommand);
            }

            AddCommand(newCommand);
        }

        private static void CommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            FormControls fc = (FormControls)dependencyObject;
            fc.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            CancelCommand?.Execute(CancelCommandParameter);
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            ResetCommand?.Execute(ResetCommandParameter);
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            SubmitCommand?.Execute(SubmitCommandParameter);
        }
    }
}
