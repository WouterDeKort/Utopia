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
    public class ResetPasswordConfirmationModel : PageBaseModel
    {
        public ResetPasswordConfirmationModel(IRepository repository,
            IFeatureToggleRepository featureToggleRepository,
            IApplicationMonitor applicationMonitor) :
            base(repository, featureToggleRepository, applicationMonitor)
        { }

        public void OnGet()
        {

        }
    }
}
