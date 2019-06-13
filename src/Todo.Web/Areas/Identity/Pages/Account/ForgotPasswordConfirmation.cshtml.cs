using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Web.Pages;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace ToDo.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageBaseModel
    {
        public ForgotPasswordConfirmation(
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
