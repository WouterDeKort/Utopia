using LaunchDarkly.Client;
using System;
using ToDo.Core.Interfaces;

namespace ToDo.Infrastructure
{
	public class FeatureToggleRepository : IFeatureToggleRepository
	{
		private readonly LdClient client;

		public FeatureToggleRepository(string launchDarkleyApiKey)
		{
			if (string.IsNullOrWhiteSpace(launchDarkleyApiKey)) throw new ArgumentNullException(nameof(launchDarkleyApiKey));

			client = new LdClient(launchDarkleyApiKey);
		}

		private static LaunchDarkly.Client.User GetUser(Core.Entities.User user)
		{
			if (user == null)
			{
				return LaunchDarkly.Client.User.WithKey("anonymous").AndAnonymous(true);
			}
			else
			{
				var firstName = user.Name.Substring(0, user.Name.IndexOf(" "));
				var lastName = user.Name.Substring(user.Name.IndexOf(" "));

				return LaunchDarkly.Client.User.WithKey(user.Email)
								.AndFirstName(firstName)
								.AndLastName(lastName);
			}
		}

		public bool StatisicsIsEnabled(Core.Entities.User user)
		{
			return client.BoolVariation("statistics", GetUser(user), false);
		}
	}
}
