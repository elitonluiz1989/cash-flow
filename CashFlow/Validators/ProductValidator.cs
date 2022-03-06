using CashFlow.Entities;
using CashFlow.Shared.Globalization;
using FluentValidation;

namespace CashFlow.Validators
{
    public class ProductValidator : BaseValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(200)
                .WithName(Strings.Name);

            RuleFor(p => p.Quantity)
                .GreaterThan((ushort)0)
                .WithName(Strings.Quantity);

            RuleFor(p => p.Price)
                .NotEqual(0)
                .WithName(Strings.Price);
        }
    }
}
