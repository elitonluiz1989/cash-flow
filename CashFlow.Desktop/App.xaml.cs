using CashFlow.Desktop.Services.Main;
using CashFlow.Desktop.ViewModels.Main;
using CashFlow.Desktop.ViewModels.Stock.StockItems;
using CashFlow.Desktop.Views.Main;
using CashFlow.Infra.Data.Context;
using CashFlow.Tools;
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

            SetDataContexts(services);

            SetViewModels(services);

            SetServices(services);

            SetWindows(services);

            _serviceProvider = services.BuildServiceProvider();
        }

        public async void AppStartup(object sender, StartupEventArgs e)
        {
            SQLiteContext dbContext = _serviceProvider.GetRequiredService<SQLiteContext>();
            await dbContext.InitializeDatabase();

            ServiceProviderAcessor.Initialize(_serviceProvider);


            MapperHandler.Initialize();

            NavigationService navigationService = _serviceProvider.GetRequiredService<NavigationService>();
            navigationService.SetCurrentViewModel(_serviceProvider.GetRequiredService<StockItemsViewViewModel>());

            MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void SetDataContexts(IServiceCollection services)
        {
            services.AddSingleton<SQLiteContext>();
        }

        private static void SetViewModels(IServiceCollection services)
        {
            services.AddTransient<StockItemsViewViewModel>();
            services.AddSingleton<MainVindowViewModel>();
        }

        private static void SetServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService>();
        }

        private static void SetWindows(IServiceCollection services)
        {
            services.AddSingleton(s => new MainWindow() { DataContext = s.GetRequiredService<MainVindowViewModel>() });
        }
    }
}
