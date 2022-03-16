using Hospital.ASP.Filters;
using Hospital.ASP.Services;
using Hospital.Domain.Security;
using Hospital.Domain.Services;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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

            services.AddDbContext<HospitalDbContext>(o => o.UseNpgsql(npgSqlConnectionString));
            services.AddSingleton<IDbContextFactory<HospitalDbContext>>(_ => new NpgSqlDbContextFactory(npgSqlConnectionString));

            //services.AddDbContext<HospitalDbContext>(o => o.UseSqlServer(localConnectionString));
            //services.AddSingleton<IDbContextFactory<HospitalDbContext>>(_ => new LocalDBContextFactory(localConnectionString));
            #endregion

            services.AddTransient<IPasswordHasher, DefaultPasswordHasher>();

            services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton(typeof(IAuthenticationService<>), typeof(AuthenticationService<>));
            services.AddSingleton<ITestDataService, TestDataService>();
            services.AddSingleton<ITherapyDataService, TherapyDataService>();

            services.AddSingleton<AmbulatoryDataService>();
            services.AddSingleton<ScheduleDataService>();
            services.AddSingleton<EntryDataService>();

            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<CheckCookieServiceFilter>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.ExpireTimeSpan = TimeSpan.FromDays(14.0);
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //env.EnvironmentName = "Production";

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
            app.UseCookiePolicy();

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
