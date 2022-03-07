using CashFlow.Desktop.Commands.Products;
using CashFlow.Desktop.ViewModels.Contract;
using CashFlow.Entities;
using CashFlow.Infra.Data.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CashFlow.Desktop.ViewModels.Products
{
    internal class ProductsViewModel : ViewModel
    {
        public ICommand ShowFormCommand { get; set; }
        public ObservableCollection<ProductViewModel>? Products { get; set; }
        private ProductsRepository _repository;

        public ProductsViewModel(
            ProductsFormShowCommand showDialogCommand,
            ProductsRepository repository
        )
        {
            ShowFormCommand = showDialogCommand;
            _repository = repository;

            LoadProducts();
        }

        private void LoadProducts()
        {
            Products = new ObservableCollection<ProductViewModel>();

            IEnumerable<Product> products = _repository.All();

            foreach (Product product in products)
            {
                Products.Add(CreateFrom<Product, ProductViewModel>(product));
            }
        }
    }
}
