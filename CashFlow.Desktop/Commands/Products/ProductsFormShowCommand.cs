using CashFlow.Desktop.Commands.Contracts;
using CashFlow.Desktop.Services.Products;

namespace CashFlow.Desktop.Commands.Products
{
    internal class ProductsFormShowCommand : Command
    {
        private readonly ProductFormService _service;

        public ProductsFormShowCommand(ProductFormService service)
        {
            _service = service;
        }

        public override void Execute(object? parameter)
        {
            _service.OpenForm();
        }
    }
}
