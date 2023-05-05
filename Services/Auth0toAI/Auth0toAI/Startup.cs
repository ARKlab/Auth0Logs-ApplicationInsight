using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Auth0toAI.Service;

[assembly: FunctionsStartup(typeof(Auth0EventGridFunction.Startup))]
namespace Auth0EventGridFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
               .AddEnvironmentVariables()
               .Build();

            builder.Services.AddSingleton<Auth0Service>();
        }
    }
}