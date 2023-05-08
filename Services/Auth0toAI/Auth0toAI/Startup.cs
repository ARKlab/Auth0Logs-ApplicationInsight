using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Auth0toAI.Service;

[assembly: FunctionsStartup(typeof(Auth0toAI.Startup))]
namespace Auth0toAI
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<Auth0Service>();
        }
    }
}