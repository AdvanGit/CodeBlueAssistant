using Hospital.Domain.Security;
using Hospital.Domain.Services;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Factories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

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
            #region DATABASE
            string localConnectionString = Configuration.GetConnectionString("local");
            string npgSqlConnectionString = Configuration.GetConnectionString("npgSql");

            //services.AddDbContext<HospitalDbContext>(o => o.UseNpgsql(npgSqlConnectionString));
            //services.AddSingleton<IDbContextFactory<HospitalDbContext>>(_ => new NpgSqlDbContextFactory(npgSqlConnectionString));

            services.AddDbContext<HospitalDbContext>(o => o.UseSqlServer(localConnectionString));
            services.AddSingleton<IDbContextFactory<HospitalDbContext>>(_ => new LocalDBContextFactory(localConnectionString));
            #endregion

            #region DATA LAYER FROM WPF ASSEMBLY
            services.AddSingleton<IPasswordHasher, TestPasswordHasher>();

            services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton(typeof(IAuthenticationService<>), typeof(AuthenticationService<>));
            services.AddSingleton<ITestDataService, TestDataService>();
            services.AddSingleton<ITherapyDataService, TherapyDataService>();

            services.AddSingleton<AmbulatoryDataService>();
            services.AddSingleton<ScheduleDataService>();
            services.AddSingleton<EntryDataService>();

            services.AddSingleton<IRootViewModelFactory, RootViewModelFactory>();
            services.AddSingleton<AmbulatoryViewModelFactory>();
            #endregion

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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

            app.UseAuthentication();
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
