using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ToDo.Core.Interfaces
{
    public interface IApplicationMonitor
    {
        //
        // Summary:
        //     This method is an internal part of Application Insights infrastructure. Do not
        //     call.
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Track(ITelemetry telemetry);
        //
        // Summary:
        //     Send information about external dependency call in the application. Create a
        //     separate Microsoft.ApplicationInsights.DataContracts.DependencyTelemetry instance
        //     for each call to Microsoft.ApplicationInsights.TelemetryClient.TrackDependency(Microsoft.ApplicationInsights.DataContracts.DependencyTelemetry)
        void TrackDependency(DependencyTelemetry telemetry);
        //
        // Summary:
        //     Send information about external dependency call in the application.
        //
        // Parameters:
        //   dependencyName:
        //     External dependency name.
        //
        //   commandName:
        //     Dependency call command name.
        //
        //   startTime:
        //     The time when the dependency was called.
        //
        //   duration:
        //     The time taken by the external dependency to handle the call.
        //
        //   success:
        //     True if the dependency call was handled successfully.
        void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration, bool success);
        //
        // Summary:
        //     Send an Microsoft.ApplicationInsights.DataContracts.EventTelemetry for display
        //     in Diagnostic Search and aggregation in Metrics Explorer. Create a separate Microsoft.ApplicationInsights.DataContracts.EventTelemetry
        //     instance for each call to Microsoft.ApplicationInsights.TelemetryClient.TrackEvent(Microsoft.ApplicationInsights.DataContracts.EventTelemetry).
        //
        // Parameters:
        //   telemetry:
        //     An event log item.
        void TrackEvent(EventTelemetry telemetry);
        //
        // Summary:
        //     Send an Microsoft.ApplicationInsights.DataContracts.EventTelemetry for display
        //     in Diagnostic Search and aggregation in Metrics Explorer.
        //
        // Parameters:
        //   eventName:
        //     A name for the event.
        //
        //   properties:
        //     Named string values you can use to search and classify events.
        //
        //   metrics:
        //     Measurements associated with this event.
        void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
        //
        // Summary:
        //     Send an Microsoft.ApplicationInsights.DataContracts.ExceptionTelemetry for display
        //     in Diagnostic Search. Create a separate Microsoft.ApplicationInsights.DataContracts.ExceptionTelemetry
        //     instance for each call to Microsoft.ApplicationInsights.TelemetryClient.TrackException(Microsoft.ApplicationInsights.DataContracts.ExceptionTelemetry)
        void TrackException(ExceptionTelemetry telemetry);
        //
        // Summary:
        //     Send an Microsoft.ApplicationInsights.DataContracts.ExceptionTelemetry for display
        //     in Diagnostic Search.
        //
        // Parameters:
        //   exception:
        //     The exception to log.
        //
        //   properties:
        //     Named string values you can use to classify and search for this exception.
        //
        //   metrics:
        //     Additional values associated with this exception.
        void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
        //
        // Summary:
        //     Send a Microsoft.ApplicationInsights.DataContracts.MetricTelemetry for aggregation
        //     in Metric Explorer. Create a separate Microsoft.ApplicationInsights.DataContracts.MetricTelemetry
        //     instance for each call to Microsoft.ApplicationInsights.TelemetryClient.TrackMetric(Microsoft.ApplicationInsights.DataContracts.MetricTelemetry).
        void TrackMetric(MetricTelemetry telemetry);
        //
        // Summary:
        //     Send a Microsoft.ApplicationInsights.DataContracts.MetricTelemetry for aggregation
        //     in Metric Explorer.
        //
        // Parameters:
        //   name:
        //     Metric name.
        //
        //   value:
        //     Metric value.
        //
        //   properties:
        //     Named string values you can use to classify and filter metrics.
        void TrackMetric(string name, double value, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Send information about the page viewed in the application.
        //
        // Parameters:
        //   name:
        //     Name of the page.
        void TrackPageView(string name);
        //
        // Summary:
        //     Send information about the page viewed in the application. Create a separate
        //     Microsoft.ApplicationInsights.DataContracts.PageViewTelemetry instance for each
        //     call to Microsoft.ApplicationInsights.TelemetryClient.TrackPageView(Microsoft.ApplicationInsights.DataContracts.PageViewTelemetry).
        void TrackPageView(PageViewTelemetry telemetry);
        //
        // Summary:
        //     Send information about a request handled by the application. Create a separate
        //     Microsoft.ApplicationInsights.DataContracts.RequestTelemetry instance for each
        //     call to Microsoft.ApplicationInsights.TelemetryClient.TrackRequest(Microsoft.ApplicationInsights.DataContracts.RequestTelemetry).
        void TrackRequest(RequestTelemetry request);
        //
        // Summary:
        //     Send information about a request handled by the application.
        //
        // Parameters:
        //   name:
        //     The request name.
        //
        //   startTime:
        //     The time when the page was requested.
        //
        //   duration:
        //     The time taken by the application to handle the request.
        //
        //   responseCode:
        //     The response status code.
        //
        //   success:
        //     True if the request was handled successfully by the application.
        void TrackRequest(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success);
        //
        // Summary:
        //     Send a trace message for display in Diagnostic Search. Create a separate Microsoft.ApplicationInsights.DataContracts.TraceTelemetry
        //     instance for each call to Microsoft.ApplicationInsights.TelemetryClient.TrackTrace(Microsoft.ApplicationInsights.DataContracts.TraceTelemetry).
        //
        // Parameters:
        //   telemetry:
        //     Message with optional properties.
        void TrackTrace(TraceTelemetry telemetry);
        //
        // Summary:
        //     Send a trace message for display in Diagnostic Search.
        //
        // Parameters:
        //   message:
        //     Message to display.
        void TrackTrace(string message);
        //
        // Summary:
        //     Send a trace message for display in Diagnostic Search.
        //
        // Parameters:
        //   message:
        //     Message to display.
        //
        //   properties:
        //     Named string values you can use to search and classify events.
        void TrackTrace(string message, IDictionary<string, string> properties);
        //
        // Summary:
        //     Send a trace message for display in Diagnostic Search.
        //
        // Parameters:
        //   message:
        //     Message to display.
        //
        //   severityLevel:
        //     Trace severity level.
        void TrackTrace(string message, SeverityLevel severityLevel);
        //
        // Summary:
        //     Send a trace message for display in Diagnostic Search.
        //
        // Parameters:
        //   message:
        //     Message to display.
        //
        //   severityLevel:
        //     Trace severity level.
        //
        //   properties:
        //     Named string values you can use to search and classify events.
        void TrackTrace(string message, SeverityLevel severityLevel, IDictionary<string, string> properties);
    }
}
