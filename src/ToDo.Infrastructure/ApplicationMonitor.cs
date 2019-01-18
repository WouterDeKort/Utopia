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
        private static TelemetryClient client = new TelemetryClient();

        public void Track(ITelemetry telemetry)
        {
            client.Track(telemetry);
        }

        public void TrackDependency(DependencyTelemetry telemetry)
        {
            client.TrackDependency(telemetry);
        }

        public void TrackDependency(string dependencyName, string commandName,
            DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            client.TrackDependency(dependencyName, commandName, startTime, duration, success);
        }

        public void TrackEvent(EventTelemetry telemetry)
        {
            client.TrackEvent(telemetry);
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null)
        {
            client.TrackEvent(eventName, properties, metrics);
        }

        public void TrackException(ExceptionTelemetry telemetry)
        {
            client.TrackException(telemetry);
        }

        public void TrackException(Exception exception,
            IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null)
        {
            client.TrackException(exception, properties, metrics);
        }

        public void TrackMetric(MetricTelemetry telemetry)
        {
            client.TrackMetric(telemetry);
        }

        public void TrackMetric(string name, double value,
            IDictionary<string, string> properties = null)
        {
            client.TrackMetric(name, value, properties);
        }

        public void TrackPageView(PageViewTelemetry telemetry)
        {
            client.TrackPageView(telemetry);
        }

        public void TrackPageView(string name)
        {
            client.TrackPageView(name);
        }

        public void TrackRequest(RequestTelemetry request)
        {
            client.TrackRequest(request);
        }

        public void TrackRequest(string name,
            DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success)
        {
            client.TrackRequest(name, startTime, duration, responseCode, success);
        }

        public void TrackTrace(string message)
        {
            client.TrackTrace(message);
        }

        public void TrackTrace(TraceTelemetry telemetry)
        {
            client.TrackTrace(telemetry);
        }

        public void TrackTrace(string message, SeverityLevel severityLevel)
        {
            client.TrackTrace(message, severityLevel);
        }

        public void TrackTrace(string message, IDictionary<string, string> properties)
        {
            client.TrackTrace(message, properties);
        }

        public void TrackTrace(string message, SeverityLevel severityLevel,
            IDictionary<string, string> properties)
        {
            client.TrackTrace(message, severityLevel, properties);
        }
    }
}
