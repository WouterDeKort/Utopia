using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ToDo.Core.Entities;

namespace ToDo.Infrastructure.Identity
{
	public class IdentityDbContextSeed
	{
		public static async Task SeedAsync(UserManager<User> userManager)
		{
			var defaultUser = new User { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
			await userManager.CreateAsync(defaultUser, "Pass@word1");
		}
	}
}
