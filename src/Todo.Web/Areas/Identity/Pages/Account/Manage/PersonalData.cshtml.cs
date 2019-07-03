using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ToDo.Core.Entities;

namespace ToDo.Web.Areas.Identity.Pages.Account.Manage
{
	public class PersonalDataModel : PageModel
	{
		private readonly UserManager<User> _userManager;

		public PersonalDataModel(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IActionResult> OnGet()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			return Page();
		}
	}
}