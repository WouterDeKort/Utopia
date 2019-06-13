using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Infrastructure.Data;

namespace ToDo.Tests.Integration.Data
{
	public static class RepositoryHelper
	{
		private static DbContextOptions<AppDbContext> CreateNewContextOptions()
		{
			var serviceProvider = new ServiceCollection()
				.AddEntityFrameworkInMemoryDatabase()
				.BuildServiceProvider();

			var builder = new DbContextOptionsBuilder<AppDbContext>();
			builder.UseInMemoryDatabase("ToDo")
				   .UseInternalServiceProvider(serviceProvider);

			return builder.Options;
		}

		public static EfRepository GetRepository()
		{
			var options = CreateNewContextOptions();
			var _dbContext = new AppDbContext(options);

			return new EfRepository(_dbContext);
		}
	}
}
