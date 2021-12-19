using CashFlow.Desktop.Tools;
using CashFlow.Infra.Data.Mapping;
using System.ComponentModel;

namespace CashFlow.Desktop.ViewModels.Contract
{
    internal class ViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose() { }

        public TType CastTo<TType>()
        {
            return AppMapper.Map<TType>(this);
        }

        public static TViewModel CreateFrom<TType, TViewModel>(TType obj)
        {
            if (obj == null)
                return ServiceProviderAcessor.GetRequiredService<TViewModel>();

            return AppMapper.Map<TViewModel>(obj);
        }
    }
}
