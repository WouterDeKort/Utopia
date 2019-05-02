using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using System.Reflection;
using ToDo.Core.Interfaces;
using ToDo.Core.SharedKernel;
using ToDo.Infrastructure;
using ToDo.Infrastructure.Data;
using ToDo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Http;
using ToDo.Infrastructure.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Todo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            AddDatabase(services);

            ConfigureCookieSettings(services);

            CreateIdentityIfNotCreated(services);

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            string sendGridApiKey = Configuration["SendGrid:ApiKey"];
            services.AddTransient<IEmailSender>(o => new EmailSender(sendGridApiKey));

            services.AddMvc()
                .AddControllersAsServices()
                .AddSessionStateTempDataProvider()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizePage("/Add");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSession();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            return BuildDependencyInjectionProvider(services);
        }

        private static void CreateIdentityIfNotCreated(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var existingUserManager = scope.ServiceProvider
                    .GetService<UserManager<User>>();
                if (existingUserManager == null)
                {
                    services.AddIdentity<User, IdentityRole>()
                        .AddDefaultUI(UIFramework.Bootstrap4)
                        .AddEntityFrameworkStores<IdentityDbContext>()
                                        .AddDefaultTokenProviders();
                }
            }
        }

        private static void ConfigureCookieSettings(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Signout";
                options.Cookie = new CookieBuilder
                {
                    IsEssential = true
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, AppDbContext context)
        {
            AddLogging(loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();

            app.UseSession();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "identity",
                    template: "Identity/{controller=Account}/{action=Register}/{id?}");


                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
            }
        }

        protected virtual void AddDatabase(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Utopia")));

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
        }

        protected virtual void AddLogging(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
        }

        public IServiceProvider BuildDependencyInjectionProvider(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            // Populate the container using the service collection
            builder.Populate(services);

            Assembly webAssembly = Assembly.GetExecutingAssembly();
            Assembly coreAssembly = Assembly.GetAssembly(typeof(BaseEntity));
            Assembly infrastructureAssembly = Assembly.GetAssembly(typeof(EfRepository));

            string launchDarkleyApiKey = Configuration["LaunchDarkley:ApiKey"];
            builder.RegisterType<FeatureToggleRepository>()
                   .As<IFeatureToggleRepository>()
                   .WithParameter("launchDarkleyApiKey", launchDarkleyApiKey)
                  .SingleInstance();

            //string sendGridApiKey = Configuration["SendGrid:ApiKey"];
            //builder.RegisterType<EmailSender>()
            //       .As<IEmailSender>()
            //       .WithParameter("apiKey", sendGridApiKey)
            //      .SingleInstance();

            string applicationInsightsApiKey = Configuration["ApplicationInsights:InstrumentationKey"];
            builder.RegisterType<ApplicationMonitor>()
                   .As<IApplicationMonitor>()
                   .WithParameter("key", applicationInsightsApiKey)
                  .SingleInstance();

            builder.RegisterAssemblyTypes(
                webAssembly,
                coreAssembly,
                infrastructureAssembly)
                .Except<FeatureToggleRepository>()
                .Except<ApplicationMonitor>()
                .Except<EmailSender>()
                .AsImplementedInterfaces();

            IContainer applicationContainer = builder.Build();
            return new AutofacServiceProvider(applicationContainer);
        }
    }
}
