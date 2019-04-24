using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;

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
            IRepository repository,
            IFeatureToggleRepository featureToggleRepository,
            IApplicationMonitor applicationMonitor) :
            base(repository, featureToggleRepository, applicationMonitor)
        {
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            ViewData["StatisicsIsEnabled"] = _featureToggleRepository.StatisicsIsEnabled();
            base.OnPageHandlerExecuting(context);
        }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            _applicationMonitor.TrackEvent("Overview page loaded",
               new Dictionary<string, string> { { "page", pageNumber.ToString() } });

            var result = await _repository.PageAsync<ToDoItem>(pageNumber, 10);

            ToDoItems = result.Items;
            NumberOfPages = result.NumberOfPages;
            CurrentPage = pageNumber;
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            throw new ArgumentException("My Test exception!");

            //var item = await _repository.GetByIdAsync<ToDoItem>(id);

            //if (item != null)
            //{
            //    await _repository.DeleteAsync(item);
            //}

            //Message = $"ToDo {id} deleted successfully";

            //return RedirectToPage();
        }
    }
}
