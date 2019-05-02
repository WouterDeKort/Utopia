using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Web.Pages;
using ToDo.Core.Interfaces;

namespace ToDo.Web.Areas.Identity.Pages.Account
{
    public class AccessDeniedModel : PageBaseModel
    {
        public AccessDeniedModel(IRepository repository,
            IFeatureToggleRepository featureToggleRepository,
            IApplicationMonitor applicationMonitor) :
            base(repository, featureToggleRepository, applicationMonitor)
        { }

        public void OnGet()
        {

        }
    }
}

