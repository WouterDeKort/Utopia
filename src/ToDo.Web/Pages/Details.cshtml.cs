using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;

namespace ToDo.Web.Pages.ToDoRazorPage
{
	public class DetailsModel : PageModel
	{
		private readonly IRepository repository;
		private readonly IApplicationMonitor applicationMonitor;

		public ToDoItem ToDoItem { get; set; }

		public DetailsModel(
			IRepository repository,
			IApplicationMonitor applicationMonitor)
		{
			this.repository = repository;
			this.applicationMonitor = applicationMonitor;
		}

		public async Task OnGetAsync(int id)
		{
			applicationMonitor.TrackEvent("Detail page loaded",
			   new Dictionary<string, string> { { "item-id", id.ToString() } });

			ToDoItem = await repository.GetByIdAsync<ToDoItem>(id);
		}
	}
}
