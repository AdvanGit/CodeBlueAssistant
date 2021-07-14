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
            new Main(serviceProvider.GetRequiredService<IRootViewModelFactory>()).Show();

            base.OnStartup(e);
        }

        private IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<HospitalDbContextFactory>();
            services.AddSingleton<IRootViewModelFactory, RootViewModelFactory>();
            services.AddSingleton<AmbulatoryViewModelFactory>();

            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            services.AddSingleton(typeof(IDataServices<>), typeof(GenericDataService<>));
            services.AddSingleton<ITestDataService, TestDataService>();
            services.AddSingleton<ITherapyDataService, TherapyDataService>();
            services.AddSingleton<AmbulatoryDataService>();
            services.AddSingleton<ScheduleDataService>();
            services.AddSingleton<EntryDataService>();

            return services.BuildServiceProvider();
        }
    }
}
