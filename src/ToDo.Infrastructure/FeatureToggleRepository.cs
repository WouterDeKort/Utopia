using LaunchDarkly.Client;
using System;
using ToDo.Core.Interfaces;

namespace ToDo.Infrastructure
{
    public class FeatureToggleRepository : IFeatureToggleRepository
    {
        private static Lazy<LdClient> client = new Lazy<LdClient>(() =>
        {
            string key = "";// System.Configuration.ConfigurationManager.AppSettings["LaunchDarkleyKey"];
            return new LdClient(key);
        });

        public bool StatisicsIsEnabled()
        {
            //return client.Value.BoolVariation("statistics", GetUser(), false);
            return false;
        }

        private static User GetUser()
        {
            return null;
            //return User.WithKey("wouter@example.com")
            //                .AndFirstName("Wouter")
            //                .AndLastName("de Kort")
            //                .AndCustomAttribute("groups", "beta_testers");
        }
    }
}
