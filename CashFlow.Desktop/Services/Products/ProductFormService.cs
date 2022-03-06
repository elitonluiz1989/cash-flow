using CashFlow.Desktop.ViewModels.Products;
using CashFlow.Desktop.Views.Products;
using CashFlow.Shared;

namespace CashFlow.Desktop.Services.Products
{
    internal class ProductFormService
    {
        private ProductForm? _productForm;

        public void OpenForm()
        {
            _productForm = new();
            _productForm.DataContext = ServiceProviderAcessor.GetRequiredService<ProductViewModel>();
            _productForm.ShowDialog();
        }

        public void CloseForm()
        {
            _productForm?.Close();
        }
    }
}
