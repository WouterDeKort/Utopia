using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ToDo.Core.Entities;

namespace ToDo.Web.Middleware
{
	public class AuthenticatedTestRequestMiddleware
	{
		public const string TestingCookieAuthentication = "TestCookieAuthentication";
		public const string TestingHeader = "X-Integration-Testing";
		public const string TestingHeaderValue = "abcde-12345";

		private readonly RequestDelegate _next;

		public AuthenticatedTestRequestMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			if (context.Request.Headers.Keys.Contains(TestingHeader) &&
				context.Request.Headers[TestingHeader].First().Equals(TestingHeaderValue))
			{
				if (context.Request.Headers.Keys.Contains("my-name"))
				{
					var name =
						context.Request.Headers["my-name"].First();
					var id =
						context.Request.Headers.Keys.Contains("my-id")
							? context.Request.Headers["my-id"].First() : "";

					ClaimsIdentity claimsIdentity = new ClaimsIdentity(
						new List<Claim>
							{
								new Claim(ClaimTypes.Name, name),
								new Claim(ClaimTypes.NameIdentifier, id),
							},
						TestingCookieAuthentication);

					ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
					context.User = claimsPrincipal;
				}
			}

			await _next(context);
		}
	}
}
