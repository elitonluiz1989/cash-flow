using CashFlow.Tools;
using System;
using System.ComponentModel;

namespace CashFlow.Desktop.ViewModels.Contract
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose() { }

        public TType CastTo<TType>()
        {
            return MapperHandler.Map<TType>(this);
        }

        public static TViewModel CreateFrom<TType, TViewModel>(TType obj) where TViewModel : notnull
        {
            if (obj == null)
                return ServiceProviderAcessor.GetRequiredService<TViewModel>();

            return MapperHandler.Map<TViewModel>(obj);
        }

        public virtual void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
