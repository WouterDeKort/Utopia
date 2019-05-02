//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System.IO;
//using ToDo.Infrastructure.Data;

//namespace ToDo.Infrastructure
//{
//    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//    {
//        public AppDbContext CreateDbContext(string[] args)
//        {
//            //IConfigurationRoot configuration = new ConfigurationBuilder()
//            //    .SetBasePath(Directory.GetCurrentDirectory())
//            //    .AddJsonFile("appsettings.json")
//            //    .Build();

//            var connectionString = "Server = (localdb)\\MSSQLLocalDB; Initial Catalog = ToDo; Trusted_Connection = True; MultipleActiveResultSets = true"; // configuration.GetConnectionString("Utopia");

//            var builder = new DbContextOptionsBuilder<AppDbContext>();
            
//            builder.UseSqlServer(connectionString, options=> options.MigrationsAssembly("ToDo.Infrastructure"));
//            return new AppDbContext(builder.Options, null);
//        }
//    }
//}
