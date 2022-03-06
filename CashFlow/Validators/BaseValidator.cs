using FluentValidation;
using FluentValidation.Results;

namespace CashFlow.Validators
{
    public abstract class BaseValidator<TEntity> : AbstractValidator<TEntity>
        where TEntity : class
    {
        public ValidationResult ExecuteRules(TEntity entity, string[] propertiesName)
        {
            return DefaultValidatorExtensions.Validate(this, entity, opt => opt.IncludeProperties(propertiesName));
        }

        public ValidationResult ExecuteRule(TEntity entity, string propertyName)
        {
            string[] propertiesName = new string[] { propertyName };
            return ExecuteRules(entity, propertiesName);
        }
    }
}
