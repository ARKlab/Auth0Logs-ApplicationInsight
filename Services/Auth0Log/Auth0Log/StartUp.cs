using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights.Extensibility;

[assembly: FunctionsStartup(typeof(Auth0EventGridFunction.StartUp))]
namespace Auth0EventGridFunction
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var _config = new ConfigurationBuilder()
               .AddEnvironmentVariables()
               .Build();

            var telemetryClient = new TelemetryClient(new TelemetryConfiguration(_config["AUTH0_INSTRUMENTATION_KEY"]));

            builder.Services.AddSingleton(telemetryClient);
        }
    }
}