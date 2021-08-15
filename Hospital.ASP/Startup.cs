using Hospital.Domain.Services;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Factories;
using Hospital.ViewModel.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ASP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("default");
            services.AddDbContext<HospitalDbContext>(options =>
                options.UseNpgsql(connection));

            services.AddSingleton<IDbContextFactory<HospitalDbContext>>(_ => new NpgSqlDbContextFactory(connection));


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

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
