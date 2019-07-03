using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.Core.Entities;

namespace ToDo.Infrastructure.Identity
{
	public class IdentityDbContext : IdentityDbContext<User>
	{
		public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
			: base(options)
		{
		}
	}
}
