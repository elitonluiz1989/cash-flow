using CashFlow.Desktop.Commands.Contracts;
using CashFlow.Desktop.Services.Products;
using CashFlow.Desktop.ViewModels.Products;
using CashFlow.Entities;
using CashFlow.Infra.Data.Repositories;
using CashFlow.Shared;
using CashFlow.Shared.Globalization;
using System;
using System.Windows;

namespace CashFlow.Desktop.Commands.Products
{
    internal class ProductsSaveCommand : Command
    {
        private readonly ProductsRepository _repository;
        private readonly ProductFormService _service;

        public ProductsSaveCommand(
            ProductFormService service,
            ProductsRepository repository
        )
        {
            _service = service;
            _repository = repository;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is ProductViewModel viewModel)
            {
                try
                {
                    viewModel.ValidateAll();

                    if (!viewModel.HasErrors)
                    {
                        Product entity = viewModel.CastTo<Product>();
                        _repository.Save(entity);

                        MessageBox.Show(Strings.ProductSaved);

                        _service.CloseForm();
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show(Helpers.GetExceptionMesssage(ex));
                }
            }
        }
    }
}
