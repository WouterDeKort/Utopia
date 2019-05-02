using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Web.Pages;
using ToDo.Core.Interfaces;

namespace ToDo.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LockoutModel : PageBaseModel
    {
        public LockoutModel(IRepository repository,
            IFeatureToggleRepository featureToggleRepository,
            IApplicationMonitor applicationMonitor) :
            base(repository, featureToggleRepository, applicationMonitor)
        { }

        public void OnGet()
        {

        }
    }
}
