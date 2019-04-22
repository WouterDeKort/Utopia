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
using System.Reflection;
using ToDo.Core.Interfaces;
using ToDo.Core.SharedKernel;
using ToDo.Infrastructure;
using ToDo.Infrastructure.Data;

namespace ToDo.Web
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            AddDatabase(services);

            services.AddMvc()
                .AddControllersAsServices()
                .AddSessionStateTempDataProvider()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSession();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            return BuildDependencyInjectionProvider(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, AppDbContext context)
        {
            AddLogging(loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc(routes =>
            {
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

            builder.RegisterAssemblyTypes(
                webAssembly, 
                coreAssembly, 
                infrastructureAssembly)
                .Except<FeatureToggleRepository>()
                .AsImplementedInterfaces();

            IContainer applicationContainer = builder.Build();
            return new AutofacServiceProvider(applicationContainer);
        }
    }
}
