using CashFlow.Validators;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CashFlow.Desktop.ViewModels.Contract
{
    internal class ViewModelWithValidation<TEntity> : ViewModel, INotifyDataErrorInfo
        where TEntity : class
    {
        public bool HasErrors => _errors.Any();
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        private readonly BaseValidator<TEntity> Validator;
        private readonly Dictionary<string, List<string>> _errors = new();

        public ViewModelWithValidation(BaseValidator<TEntity> validator)
        {
            Validator = validator;
        }

        public new void OnPropertyChanged(string propertyName)
        {
            Validate(propertyName);
            base.OnPropertyChanged(propertyName);
        }

        public void Validate(string propertyName)
        {
            TEntity entity = CastTo<TEntity>();

            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                ValidationResult results = Validator.ExecuteRule(entity, propertyName);
                ClearErrors(propertyName);                    
                AddErrors(results);
            }
        }

        public void ValidateAll()
        {
            TEntity entity = CastTo<TEntity>();
            ValidationResult results = Validator.Validate(entity);

            AddErrors(results);
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName) || !_errors.ContainsKey(propertyName))
                return Enumerable.Empty<string>();

            return _errors[propertyName];
        }

        public Dictionary<string, List<string>> GetAllErrors() => _errors;

        public void AddErrors(ValidationResult results)
        {
            if (results != null && !results.IsValid)
            {
                foreach (ValidationFailure error in results.Errors)
                {
                    AddError(error.PropertyName, error.ErrorMessage);
                }
            }
        }

        public void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        public void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
