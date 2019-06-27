using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Todo.Web;
using ToDo.Core.Entities;
using ToDo.Infrastructure.Data;
using ToDo.Web;

namespace ToDo.Tests.Integration.Web
{
	public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			var location = Directory.GetCurrentDirectory();
			location = location.Substring(0, location.IndexOf("Utopia") + "Utopia".Length);

			builder.UseContentRoot(location + @"\src\ToDo.Web");

			builder.ConfigureTestServices(services =>
			{
				UseInMemoryDatabase(services);
				AddTestData(services);
			});
		}

		private void AddTestData(IServiceCollection services)
		{
			var sp = services.BuildServiceProvider();

			using (var scope = sp.CreateScope())
			{
				var scopedServices = scope.ServiceProvider;
				var db = scopedServices.GetRequiredService<AppDbContext>();
				var identityDb = scopedServices.GetRequiredService<Infrastructure.Identity.IdentityDbContext>();
				var logger = scopedServices
					.GetRequiredService<ILogger<CustomWebApplicationFactory>>();

				var userManager = scopedServices.GetRequiredService<UserManager<User>>();

				db.Database.EnsureCreated();

				try
				{
					_ = SeedData.PopulateTestDataAsync(db, userManager).Result;
				}
				catch (Exception ex)
				{
					logger.LogError(ex, $"An error occurred seeding the " +
						"database with test messages. Error: {ex.Message}");
				}
			}
		}

		private static void UseInMemoryDatabase(IServiceCollection services)
		{
			var serviceProvider = new ServiceCollection()
								.AddEntityFrameworkInMemoryDatabase()
								.BuildServiceProvider();

			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseInMemoryDatabase("InMemoryDbForTesting");
				options.UseInternalServiceProvider(serviceProvider);
			});

			services.AddDbContext<Infrastructure.Identity.IdentityDbContext>(options =>
			{
				options.UseInMemoryDatabase("InMemoryDbForTesting");
				options.UseInternalServiceProvider(serviceProvider);
			});
		}
	}
}
