using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Shared
{
    public class ServiceProviderAcessor
    {
        private static IServiceProvider? _serviceProvider;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetRequiredService<T>() where T : notnull
        {
            if (_serviceProvider == null)
            {
                throw new NullReferenceException();
            }

            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
