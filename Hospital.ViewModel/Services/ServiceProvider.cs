using Hospital.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hospital.ViewModel.Services
{
    internal static class ServiceProvider
    {
        static IServiceCollection services = new ServiceCollection();
        static IServiceProvider serviceProvider;

        static ServiceProvider()
        {
            services.AddSingleton<HospitalDbContextFactory>();

            serviceProvider = services.BuildServiceProvider();
        }

        public static object GetService(Type serviceType)
        {
            return serviceProvider.GetRequiredService(serviceType);
        }

        public static T GetService<T>()
        {
            return serviceProvider.GetRequiredService<T>();
        }
    }
}
