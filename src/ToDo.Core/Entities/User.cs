using Microsoft.AspNetCore.Identity;

namespace ToDo.Core.Entities
{
	// Add profile data for application users by adding properties to the User class
	public class User : IdentityUser
	{
		[PersonalDataAttribute]
		public string Name { get; set; }
	}
}
