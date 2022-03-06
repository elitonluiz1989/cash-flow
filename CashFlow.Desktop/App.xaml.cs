using AutoMapper;
using CashFlow.Desktop.Commands.Generic;
using CashFlow.Desktop.Commands.Products;
using CashFlow.Desktop.Services.Main;
using CashFlow.Desktop.Services.Products;
using CashFlow.Desktop.ViewModels.Main;
using CashFlow.Desktop.ViewModels.Products;
using CashFlow.Desktop.Views.Main;
using CashFlow.Desktop.Views.Products;
using CashFlow.Infra.Data.Context;
using CashFlow.Infra.Data.Repositories;
using CashFlow.Shared;
using CashFlow.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

            SetValidators(services);

            SetRepositories(services);

            SetCommands(services);

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

            List<Profile> profiles = new()
            {
                new MappingProfile()
            };

            MapperHandler.Initialize(profiles);

            NavigationService navigationService = _serviceProvider.GetRequiredService<NavigationService>();
            navigationService.SetCurrentViewModel(_serviceProvider.GetRequiredService<ProductsViewModel>());

            MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void SetDataContexts(IServiceCollection services)
        {
            services.AddSingleton<SQLiteContext>();
        }

        private static void SetValidators(IServiceCollection services)
        {
            services.AddTransient<ProductValidator>();
        }

        private static void SetRepositories(IServiceCollection services)
        {
            services.AddTransient<ProductsRepository>();
        }

        private static void SetCommands(IServiceCollection services)
        {
            services.AddTransient<CloseWindowCommand>();
            services.AddTransient<ResetFormCommand>();

            services.AddTransient<ProductsFormShowCommand>();
            services.AddTransient<ProductsSaveCommand>();
        }

        private static void SetViewModels(IServiceCollection services)
        {
            services.AddTransient<ProductsViewModel>();
            services.AddTransient<ProductViewModel>();
            services.AddSingleton<MainVindowViewModel>();
        }

        private static void SetServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService>();

            services.AddSingleton<ProductFormService>();
        }

        private static void SetWindows(IServiceCollection services)
        {
            services.AddTransient(s => new ProductForm() { DataContext = s.GetRequiredService<ProductViewModel>() });

            services.AddSingleton(s => new MainWindow() { DataContext = s.GetRequiredService<MainVindowViewModel>() });
        }
    }
}
