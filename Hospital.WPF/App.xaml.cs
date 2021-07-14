using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel;
using Hospital.ViewModel.Factories;
using Hospital.ViewModel.Services;
using Hospital.WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Hospital.WPF
{
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            serviceProvider = ConfigureServices();
            new Main(serviceProvider.GetRequiredService<IRootViewModelFactory<LoginViewModel>>()).Show();

            base.OnStartup(e);
        }

        private IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IRootViewModelFactory<LoginViewModel>, LoginViewModelFactory>();

            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<IDataServices<Staff>, GenericDataServices<Staff>>();

            services.AddSingleton<HospitalDbContextFactory>();

            return services.BuildServiceProvider();
        }
    }
}
