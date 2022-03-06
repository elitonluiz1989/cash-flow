using CashFlow.Entities;
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
                .MaximumLength(200);

            RuleFor(p => p.Quantity)
                .GreaterThan((ushort)0);

            RuleFor(p => p.Price)
                .NotEqual(0);
        }
    }
}
