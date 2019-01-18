using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ToDo.Web.Pages.ToDoRazorPage
{
    public class DetailsModel : PageBaseModel
    {
        public ToDoItem ToDoItem { get; set; }

        public DetailsModel(
            IRepository repository,
            IFeatureToggleRepository featureToggleRepository,
            IApplicationMonitor applicationMonitor)
            : base(repository, featureToggleRepository, applicationMonitor)
        {
        }

        public async Task OnGetAsync(int id)
        {
            _applicationMonitor.TrackEvent("Detail page loaded",
               new Dictionary<string, string> { { "item-id", id.ToString() } });

            ToDoItem = await _repository.GetByIdAsync<ToDoItem>(id);
        }
    }
}
