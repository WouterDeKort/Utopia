using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Web.Pages;
using ToDo.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using ToDo.Infrastructure.Identity;

namespace ToDo.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LockoutModel : PageBaseModel
    {
        public LockoutModel(
			UserManager<User> userManager,
			IRepository repository,
            IFeatureToggleRepository featureToggleRepository,
            IApplicationMonitor applicationMonitor) :
            base(userManager, repository, featureToggleRepository, applicationMonitor)
        { }

        public void OnGet()
        {

        }
    }
}
