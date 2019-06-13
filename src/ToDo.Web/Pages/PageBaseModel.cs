using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Identity;

namespace ToDo.Web.Pages
{
    public abstract class PageBaseModel : PageModel
    {
		protected readonly UserManager<User> _userManager;
		protected readonly IRepository _repository;
        protected readonly IFeatureToggleRepository _featureToggleRepository;
        protected readonly IApplicationMonitor _applicationMonitor;
        
        protected PageBaseModel(
			UserManager<User> userManager,
            IRepository repository,
            IFeatureToggleRepository featureToggleRepository,
            IApplicationMonitor applicationMonitor)
        {
			_userManager = userManager;
			_repository = repository;
            _featureToggleRepository = featureToggleRepository;
            _applicationMonitor = applicationMonitor;
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            ViewData["StatisicsIsEnabled"] = _featureToggleRepository.StatisicsIsEnabled();
            base.OnPageHandlerExecuting(context);
        }
    }
}
