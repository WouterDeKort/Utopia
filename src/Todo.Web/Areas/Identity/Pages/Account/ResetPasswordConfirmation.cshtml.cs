using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Identity;
using ToDo.Web.Pages;

namespace ToDo.Web.Areas.Identity.Pages.Account
{
	[AllowAnonymous]
    public class ResetPasswordConfirmationModel : PageBaseModel
    {
        public ResetPasswordConfirmationModel(
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
