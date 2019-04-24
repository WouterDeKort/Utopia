using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Core.Interfaces;

namespace ToDo.Infrastructure
{
    public class ApplicationMonitor : IApplicationMonitor
    {
        private TelemetryClient client;

        public ApplicationMonitor(TelemetryClient client)
        {
            this.client = client;
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            client.TrackEvent(eventName, properties, metrics);
        }
    }
}
