using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Auth0toAI.Service
{
    public class Auth0Service : IDisposable
    {
        private IConfiguration _config;
        private TelemetryClient _telemetryClient;
        public Auth0Service(IConfiguration config)
        {
            _config = config;

            var telemetryConfiguration = new TelemetryConfiguration();
            telemetryConfiguration.ConnectionString = _config["Auth0LogConnectionString"];
            _telemetryClient = new TelemetryClient(telemetryConfiguration);
        }

        public void TrackEventToApplicationInsight(string name, Dictionary<string, string> properties)
        {
            _telemetryClient.TrackEvent(name, properties);
        }

        public void TrackExceptionToApplicationInsight(Exception exception, Dictionary<string, string> properties)
        {
            _telemetryClient.TrackException(exception, properties);
        }

        public void Dispose()
        {
            _telemetryClient.Flush();
        }
    }
}
