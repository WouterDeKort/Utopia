using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Infrastructure.Data;

namespace ToDo.Web.Pages.ToDoRazorPage
{
	public class PopulateModel : PageModel
	{
		private readonly UserManager<User> userManager;
		private readonly AppDbContext context;

		public PopulateModel(
			UserManager<User> userManager,
			AppDbContext context)
		{
			this.userManager = userManager;
			this.context = context;
		}

		public int RecordsAdded { get; set; }

		public async Task OnGet()
		{
			RecordsAdded = await SeedData.PopulateTestDataAsync(context, userManager);
		}
	}
}
