using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureHostConfiguration((config) =>
            {
                config.AddEnvironmentVariables();
            })
            .Build();

        host.Run();
    }
}