using Microsoft.EntityFrameworkCore;
using ToDo.Core.Entities;

namespace ToDo.Infrastructure.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{

		}

		public DbSet<ToDoItem> ToDoItems { get; set; }

	}
}