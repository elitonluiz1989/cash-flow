using CashFlow.Desktop.Services.Main;
using CashFlow.Desktop.Tools;
using CashFlow.Desktop.ViewModels.Main;
using CashFlow.Desktop.ViewModels.Stock.StockItems;
using CashFlow.Desktop.Views.Main;
using CashFlow.Infra.Data.Context;
using CashFlow.Infra.Data.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace CashFlow.Desktop
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<SQLiteContext>();

            services.AddSingleton<StockItemsViewViewModel>();
            services.AddSingleton<MainVindowViewModel>();

            services.AddSingleton<NavigationService>();

            services.AddSingleton(s => new MainWindow() { DataContext = s.GetRequiredService<MainVindowViewModel>() });

            _serviceProvider = services.BuildServiceProvider();
        }

        public async void AppStartup(object sender, StartupEventArgs e)
        {
            SQLiteContext dbContext = _serviceProvider.GetRequiredService<SQLiteContext>();
            await dbContext.InitializeDatabase();

            ServiceProviderAcessor.Initialize(_serviceProvider);
            AppMapper.Initialize();

            NavigationService navigationService = _serviceProvider.GetRequiredService<NavigationService>();
            navigationService.SetCurrentViewModel(_serviceProvider.GetRequiredService<StockItemsViewViewModel>());

            MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
