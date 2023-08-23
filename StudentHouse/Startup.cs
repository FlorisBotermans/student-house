using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StudentHouse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace StudentHouse
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(GetConnectionStrings()[0]).UseLazyLoadingProxies());
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(GetConnectionStrings()[1]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IStudentRepository, EFStudentRepository>();
            services.AddTransient<IMealRepository, EFMealRepository>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
            app.UseMvc(routes => {
                routes.MapRoute(
                name: "default",
                template: "{controller=Meal}/{action=Index}/{id?}");
            });

            ApplicationSeedData.EnsurePopulated(app);
        }

        public string[] GetConnectionStrings()
        {
            string dbServerUsername = Environment.GetEnvironmentVariable("DBSERVER_USER");
            string dbServerPassword = Environment.GetEnvironmentVariable("DBSERVER_PASS");
            string mainConnectionString = Configuration["Data:StudentHouseMainDb:ConnectionString"];
            string identityConnectionString = Configuration["Data:StudentHouseIdentityDb:ConnectionString"];

            mainConnectionString = mainConnectionString.Replace("{your_username}", dbServerUsername).Replace("{your_password}", dbServerPassword);
            identityConnectionString = identityConnectionString.Replace("{your_username}", dbServerUsername).Replace("{your_password}", dbServerPassword);

            return new string[] { mainConnectionString, identityConnectionString };
        }
    }
}
