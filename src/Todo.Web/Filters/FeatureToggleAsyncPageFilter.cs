using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;

namespace ToDo.Web.Filters
{
	public class FeatureToggleAsyncPageFilterAttribute : ResultFilterAttribute
	{
		private readonly UserManager<User> _userManager;
		private readonly IFeatureToggleRepository _featureToggleRepository;

		public FeatureToggleAsyncPageFilterAttribute(IFeatureToggleRepository featureToggleRepository, UserManager<User> userManager)
		{
			_userManager = userManager;
			_featureToggleRepository = featureToggleRepository;
		}

		public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			if (context.Result is PageResult result)
			{
				var user = await _userManager.GetUserAsync(context.HttpContext.User);
				result.ViewData["StatisicsIsEnabled"] = _featureToggleRepository.StatisicsIsEnabled(user);
			}

			await next.Invoke();
		}

	}
}
