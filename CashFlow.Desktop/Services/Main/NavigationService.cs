using CashFlow.Desktop.ViewModels.Contract;
using CashFlow.Desktop.ViewModels.Main;

namespace CashFlow.Desktop.Services.Main
{
    internal class NavigationService
    {
        private readonly MainVindowViewModel _mainViewModel;

        public NavigationService(MainVindowViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void SetCurrentViewModel(ViewModel viewModel)
        {
            _mainViewModel.CurrentViewModel = viewModel;
        }
    }
}
