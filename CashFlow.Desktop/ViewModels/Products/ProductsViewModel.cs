using CashFlow.Desktop.Commands.Products;
using CashFlow.Desktop.ViewModels.Contract;
using System.Windows.Input;

namespace CashFlow.Desktop.ViewModels.Products
{
    internal class ProductsViewModel : ViewModel
    {
        public ICommand ShowFormCommand { get; set; }

        public ProductsViewModel(
            ProductsFormShowCommand showDialogCommand
        )
        {
            ShowFormCommand = showDialogCommand;
        }
    }
}
