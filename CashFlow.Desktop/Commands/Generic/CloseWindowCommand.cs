using CashFlow.Desktop.Commands.Contracts;
using System.Windows;

namespace CashFlow.Desktop.Commands.Generic
{
    internal class CloseWindowCommand : Command
    {
        public override void Execute(object? parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }
    }
}
