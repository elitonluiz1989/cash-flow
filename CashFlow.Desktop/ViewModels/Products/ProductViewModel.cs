using CashFlow.Desktop.Commands.Products;
using CashFlow.Desktop.Commands.Generic;
using CashFlow.Desktop.ViewModels.Contract;
using System.Windows.Input;
using CashFlow.Validators;
using CashFlow.Entities;
using System.Collections;

namespace CashFlow.Desktop.ViewModels.Products
{
    internal class ProductViewModel : ViewModelWithValidation<Product>
    {
        private string? _name;
        public string? Name {
            get => _name;
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private short? _quantity;
        public short? Quantity {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private decimal? _price;
        public decimal? Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public ICommand CancelCommand { get; set; }
        public ICommand ResetCommand { get; set;}
        public ICommand SaveCommand { get; set; }

        public ProductViewModel(
            CloseWindowCommand cancelCommand,
            ResetFormCommand resetCommand,
            ProductsSaveCommand stockItemSaveCommand,
            ProductValidator validator
        ) : base (validator)
        {
            CancelCommand = cancelCommand;
            ResetCommand = resetCommand;
            SaveCommand = stockItemSaveCommand;
        }

        public new void ValidateAll()
        {
            base.ValidateAll();
        }

        public override void Reset()
        {
            Name = null;
            Quantity = null;
            Price = null;
        }
    }
}
