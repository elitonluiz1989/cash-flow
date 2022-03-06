using CashFlow.Desktop.Commands.Contracts;
using CashFlow.Desktop.ViewModels.Contract;

namespace CashFlow.Desktop.Commands.Generic
{
    internal class ResetFormCommand : Command
    {
        public override void Execute(object? parameter)
        {
            if (parameter is ViewModel viewModel)
            {
                viewModel.Reset();
            }
        }
    }
}
