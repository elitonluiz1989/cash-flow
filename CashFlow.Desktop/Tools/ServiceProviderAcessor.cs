using Microsoft.Extensions.DependencyInjection;
using System;

namespace CashFlow.Desktop.Tools
{
    internal class ServiceProviderAcessor
    {
        private static IServiceProvider _serviceProvider;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetRequiredService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
