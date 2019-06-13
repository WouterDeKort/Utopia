using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Identity;

namespace ToDo.Web.Pages
{

	public class AddModel : PageBaseModel
	{

		public AddModel(
			UserManager<User> userManager,
			IRepository repository,
			IFeatureToggleRepository featureToggleRepository,
			IApplicationMonitor applicationMonitor)
			: base(userManager, repository, featureToggleRepository, applicationMonitor)
		{
		}

		[BindProperty]
		public ToDoItem Item { get; set; }

		[TempData]
		public string Message { get; set; }

		public async Task<IActionResult> OnPost()
		{
			if (!ModelState.IsValid)
			{
				_applicationMonitor.TrackEvent("Invalid item",
					new Dictionary<string, string> {
						{
							"item", Item.ToString()
						},
						{
							"state", ModelState.ToString()
						}
					});

				return Page();
			}

			var user = await _userManager.GetUserAsync(User);
			Item.OwnerId = user.Id;
			Item.Owner = user.Name;

			await _repository.AddAsync(Item);

			_applicationMonitor.TrackEvent("Invalid item",
				new Dictionary<string, string> {
						{
							"item", Item.ToString()
						}});

			Message = "New ToDo created";

			return RedirectToPage("./Index");
		}
	}
}