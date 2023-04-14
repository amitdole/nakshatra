using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nakshatra.Benchmark.Tests;
public class TestBase
{
    public ServiceProvider ServiceProvider { get; private set; }
    public TestBase()
    {
        var serviceCollection = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(
             path: "appsettings.json",
             optional: false,
             reloadOnChange: true).Build();

        serviceCollection.AddSingleton<IConfiguration>(configuration);

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }
}