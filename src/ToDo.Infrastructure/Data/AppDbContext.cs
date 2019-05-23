using ToDo.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToDo.Core.Entities;
using ToDo.Core.SharedKernel;

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