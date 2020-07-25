using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Users;
using Recodme.RD.BoraNow.DataAccessLayer.Context;
using Recodme.RD.BoraNow.DataLayer.Users;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Options;


namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession();
            services.AddMemoryCache();

            services.AddCors(
               (servicesObjects) =>
               {
                   servicesObjects.AddPolicy("GeneralPolicy", (builder) =>
                   {
                       builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                   });
               });

            services.Configure<CookiePolicyOptions>(
              options =>
              {
                  options.CheckConsentNeeded = context => true;
                  options.MinimumSameSitePolicy = SameSiteMode.None;
              });

            services.AddIdentity<User, Role>(
               options =>
               {
                   options.User.RequireUniqueEmail = true;
               }).AddEntityFrameworkStores<BoraNowContext>();

            services.AddDbContext<BoraNowContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("Default"));
                });

            services.AddSwaggerGen(
                x => x.SwaggerDoc("v1", new OpenApiInfo() { Title = "BoraNow", Version = "v1" })
                );

            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/FeedbackViews/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/MeteoViews/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/NewsletterViews/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/QuizzesViews/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/UserViews/{1}/{0}" + RazorViewEngine.ViewExtension);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<Role> roleManager)
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
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            SetupRolesAndUsers(userManager, roleManager);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options => options.RouteTemplate = swaggerOptions.JsonRoute);
            app.UseSwaggerUI(options => options.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.ApiDescription));
        }

        public void SetupRolesAndUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (roleManager.FindByNameAsync("Visitor").Result == null) roleManager.CreateAsync(new Role() { Name = "Visitor" }).Wait();
            if (roleManager.FindByNameAsync("Company").Result == null) roleManager.CreateAsync(new Role() { Name = "Company" }).Wait();
            if (roleManager.FindByNameAsync("Admin").Result == null) roleManager.CreateAsync(new Role() { Name = "Admin" }).Wait();
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var profile = new Profile("i'm the owner of this house","anonymous");
                var abo = new AccountBusinessController(userManager, roleManager);
                var res = abo.Register("admin", "admin@restLen.com", "Admin123!#", profile, "Admin").Result;
                var roleRes = userManager.AddToRoleAsync(userManager.FindByNameAsync("admin").Result, "Admin");
            }
        }
    }
}
