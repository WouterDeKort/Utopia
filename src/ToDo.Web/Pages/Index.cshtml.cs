using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Identity;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Todo.Core.Queries;

namespace ToDo.Web.Pages.ToDoRazorPage
{
	public class IndexModel : PageBaseModel
	{
		public List<ToDoItem> ToDoItems { get; set; }
		public int NumberOfPages { get; set; }
		public int CurrentPage { get; set; }

		[TempData]
		public string Message { get; set; }

		public bool ShowMessage => !string.IsNullOrEmpty(Message);

		public IndexModel(
			UserManager<User> userManager,
			IRepository repository,
			IFeatureToggleRepository featureToggleRepository,
			IApplicationMonitor applicationMonitor) :
			base(userManager, repository, featureToggleRepository, applicationMonitor)
		{
		}

		public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
		{
			ViewData["StatisicsIsEnabled"] = _featureToggleRepository.StatisicsIsEnabled();
			base.OnPageHandlerExecuting(context);
		}

		public async Task OnGetAsync(int pageNumber = 1)
		{
			if (User.Identity.IsAuthenticated)
			{
				_applicationMonitor.TrackEvent("Overview page loaded",
				   new Dictionary<string, string> { { "page", pageNumber.ToString() } });

				var user = await _userManager.GetUserAsync(User);

				var query = new ToDoItemsByOwnerQuery(user.Id);
				var result = await _repository.PageAsync<ToDoItem>(query, pageNumber, 10);

				ToDoItems = result.Items;
				NumberOfPages = result.NumberOfPages;
				CurrentPage = pageNumber;
			}
			else
			{
				ToDoItems = new List<ToDoItem>();
				NumberOfPages = 0;
				CurrentPage = 1;
			}
		}

		public async Task<IActionResult> OnPostDelete(int id)
		{
			if (!User.Identity.IsAuthenticated) return Challenge();

			var item = await _repository.GetByIdAsync<ToDoItem>(id);

			if (item != null)
			{
				await _repository.DeleteAsync(item);
			}

			Message = $"ToDo {id} deleted successfully";

			return RedirectToPage();
		}
	}
}
