using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Data;
using Server.Data.Repository;
using DataBase.Core;
using DataBase.Services;
using Server.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Server.AuthorizationRequirements;
using Microsoft.AspNetCore.Mvc.Authorization;
using Web.Controllers;
using Microsoft.AspNetCore.Authentication;
using Web.Transformer;
using Web.CustomPolicyProvider;

namespace Server
{
    public class Startup//
    {
        private IConfigurationRoot _confString;

        public Startup(IWebHostEnvironment hostEnv)//Добавлено _confstring
        {
            _confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)//
        {
             
            services.AddDbContext<AppDBContext>(options => options.UseSqlServer (_confString.GetConnectionString("DefaultConnection")));//Добавлено
            services.AddDbContext<UserDBContext>(config => { config.UseInMemoryDatabase("Memory"); });

            //Identity registere services
            services.AddIdentity<IdentityUser, IdentityRole>(config => 
            {
                config.Password.RequiredLength = 8;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric= false;
                config.Password.RequireUppercase = false;

            })
                .AddEntityFrameworkStores<UserDBContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Home/Login";
            });

            services.AddDistributedMemoryCache();//stackoverflow
            services.AddTransient<ICourseService, CoursesService>();
            services.AddTransient<IRepository<CourseModel>, CourseRepository>();
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "Grandmas.Cookie";
                    config.LoginPath = "/Home/Authenticate";
                });
            services.AddAuthorization(config =>
            {
                /*    var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                    var defaultAuthPolicy = defaultAuthBuilder
                    .RequireAuthenticatedUser()
                    .RequireClaim(ClaimTypes.DateOfBirth)
                    .Build();

                    config.DefaultPolicy = defaultAuthPolicy;*/

                /*config.AddPolicy("Claim.DnB", policyBuilder =>
                {
                    policyBuilder.RequireClaim(ClaimTypes.DateOfBirth);
                });*/

                config.AddPolicy("Admin", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "Admin")); 

                config.AddPolicy("Claim.DnB", policyBuilder =>
                {
                    policyBuilder.RequireCustomClaim(ClaimTypes.DateOfBirth);
                });

            });
            services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, SecurityLevelHandler>();
            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();
            services.AddScoped<IAuthorizationHandler, CookieJarAuthorizationHandler>();
            services.AddScoped<IClaimsTransformation, ClaimsTransformation>();

            services.AddControllersWithViews(config =>
            {
                var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                var defaultAuthPolicy = defaultAuthBuilder
                .RequireAuthenticatedUser()
                .RequireClaim(ClaimTypes.DateOfBirth)
                .Build();

                //global auth. filter
                config.Filters.Add(new AuthorizeFilter());
            });

            services.AddRazorPages().AddRazorPagesOptions(config => 
            {
                config.Conventions.AuthorizePage("/Razor/Secured");
                config.Conventions.AuthorizePage("/Razor/Policy", "Admin");

            });
            services.AddMvc();//.AddSessionStateTempDataProvider();//stackoverflow
            services.AddMemoryCache();
            services.AddSession();//stackoverflow



            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseStaticFiles();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSession();//stackoverflow
            //Добавленные адреса
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            // who are you?
            app.UseAuthentication();
            // are you allowed?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });


            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContext content = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                DBObjects.Initial(content);
            }
        }
    }
}
