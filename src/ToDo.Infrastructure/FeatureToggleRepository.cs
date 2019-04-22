using LaunchDarkly.Client;
using System;
using ToDo.Core.Interfaces;

namespace ToDo.Infrastructure
{
    public class FeatureToggleRepository : IFeatureToggleRepository
    {
        private LdClient client;

        public FeatureToggleRepository(string launchDarkleyApiKey)
        {
            if (string.IsNullOrWhiteSpace(launchDarkleyApiKey)) throw new ArgumentNullException(nameof(launchDarkleyApiKey));

            client = new LdClient(launchDarkleyApiKey);
        }

        public bool StatisicsIsEnabled()
        {
            return client.BoolVariation("statistics", GetUser(), false);
        }

        private static User GetUser()
        {
            return User.WithKey("wouter@example.com")
                            .AndFirstName("Wouter")
                            .AndLastName("de Kort")
                            .AndCustomAttribute("groups", "beta_testers");
        }
    }
}
