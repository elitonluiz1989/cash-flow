using CashFlow.Desktop.Tools;
using CashFlow.Desktop.Views;
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

            services.AddSingleton<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();
        }

        public async void AppStartup(object sender, StartupEventArgs e)
        {
            SQLiteContext dbContext = _serviceProvider.GetRequiredService<SQLiteContext>();
            await dbContext.InitializeDatabase();

            ServiceProviderAcessor.Initialize(_serviceProvider);
            AppMapper.Initialize();

            MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
