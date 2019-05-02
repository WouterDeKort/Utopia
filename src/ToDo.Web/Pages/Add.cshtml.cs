using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;

namespace ToDo.Web.Pages
{

    public class AddModel : PageBaseModel
    {

        public AddModel(
            IRepository repository,
            IFeatureToggleRepository featureToggleRepository,
            IApplicationMonitor applicationMonitor)
            : base(repository, featureToggleRepository, applicationMonitor)
        { 
        }

    [BindProperty]
    public ToDoItem Item { get; set; }

    [TempData]
    public string Message { get; set; }

    public IActionResult OnPost()
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
        _repository.AddAsync(Item);
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