using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;

namespace Auth0toAI.Service
{
    public sealed class Auth0Service : IDisposable
    {
        private IConfiguration _config;
        private TelemetryClient _telemetryClient;
        private TelemetryConfiguration _telemetryConfiguration;

        public Auth0Service(IConfiguration config)
        {
            _config = config;

            _telemetryConfiguration = new TelemetryConfiguration();
            _telemetryConfiguration.ConnectionString = _config["Auth0LogConnectionString"];
            _telemetryClient = new TelemetryClient(_telemetryConfiguration);
        }

        public void TrackEventToApplicationInsight(string name, Dictionary<string, string> properties)
        {
            if (properties.TryGetValue("user_id", out var userId))
            {
                _telemetryClient.Context.User.Id = userId;
            }

            if (properties.TryGetValue("ip", out var ip))
            {
                _telemetryClient.Context.Location.Ip = ip;
            }

            _telemetryClient.TrackEvent(name, properties);
        }

        public void TrackExceptionToApplicationInsight(Exception exception, Dictionary<string, string> properties)
        {
            _telemetryClient.TrackException(exception, properties);
        }

        public void Dispose()
        {
            _telemetryClient.Flush();
            _telemetryConfiguration.Dispose();
        }
    }
}
