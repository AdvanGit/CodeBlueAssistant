using Hospital.Domain.Services;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Factories;
using Hospital.ViewModel.Services;
using Hospital.WPF.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Hospital.WPF
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            new Main(_host.Services.GetRequiredService<IRootViewModelFactory>()).Show();

            base.OnStartup(e);
        }

        protected async override void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(configuration =>
                {
                    configuration.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((context, services) =>
                {
                    string localConnectionString = context.Configuration.GetConnectionString("local");
                    string npqSqlConnectionString = context.Configuration.GetConnectionString("npgSql");

                    services.AddDbContext<HospitalDbContext>(o => o.UseSqlServer(localConnectionString, b => b.MigrationsAssembly("Hospital.WPF"))); //for migrations
                    //services.AddDbContext<HospitalDbContext>(o => o.UseNpgsql(npqSqlConnectionString, b => b.MigrationsAssembly("Hospital.WPF"))); //for migrations

                    services.AddSingleton<IDbContextFactory<HospitalDbContext>>(_ => new NpgSqlDbContextFactory(npqSqlConnectionString));

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
                });
        }
    }
}
