using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Data;

namespace ToDo.Web.Pages.ToDoRazorPage
{
    public class PopulateModel : PageBaseModel
    {
        private readonly AppDbContext _context;

        public PopulateModel(
            IRepository repository,
            IFeatureToggleRepository featureToggleRepository,
            IApplicationMonitor applicationMonitor,
            AppDbContext context)
            : base(repository, featureToggleRepository, applicationMonitor)
        {
            _context = context;
        }

        public int RecordsAdded { get; set; }

        public void OnGet()
        {
            RecordsAdded = SeedData.PopulateTestData(_context);
        }
    }
}
