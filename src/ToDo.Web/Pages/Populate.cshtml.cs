using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Data;
using ToDo.Infrastructure.Identity;

namespace ToDo.Web.Pages.ToDoRazorPage
{
    public class PopulateModel : PageBaseModel
    {
        private readonly AppDbContext _context;

        public PopulateModel(
			UserManager<User> userManager,
            IRepository repository,
            IFeatureToggleRepository featureToggleRepository,
            IApplicationMonitor applicationMonitor,
            AppDbContext context)
            : base(userManager, repository, featureToggleRepository, applicationMonitor)
        {
            _context = context;
        }

        public int RecordsAdded { get; set; }

        public async Task OnGet()
        {
            RecordsAdded = await SeedData.PopulateTestDataAsync(_context, _userManager);
        }
    }
}
