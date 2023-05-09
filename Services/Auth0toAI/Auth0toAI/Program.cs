using Auth0toAI.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureHostConfiguration((config) => {
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((c, s) => {
        s.AddSingleton<Auth0Service>();
    })
    .Build(); 

host.Run();