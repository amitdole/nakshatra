using Azure.Identity;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using Serilog.Formatting.Compact;

namespace PersonalWebsite.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Unhandled exception");
            }
            finally
            {
                Log.Information("Shut down complete");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddEnvironmentVariables();
                var env = context.HostingEnvironment;
                var root = config.Build();
                config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
                config.AddAzureKeyVault(new Uri($"{root["Keyvault:Uri"]}"),
       new DefaultAzureCredential(options: new DefaultAzureCredentialOptions
       {
           ExcludeAzurePowerShellCredential = true,
           ExcludeEnvironmentCredential = true,
           ExcludeInteractiveBrowserCredential = true,
           ExcludeSharedTokenCacheCredential = true,
           ExcludeVisualStudioCodeCredential = true,
           ExcludeManagedIdentityCredential = env.EnvironmentName == Environments.Development,
           ExcludeAzureCliCredential = env.EnvironmentName == Environments.Development,
           ExcludeVisualStudioCredential = true
       }));
            })
             .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .WriteTo.ApplicationInsights(new TelemetryConfiguration { ConnectionString = hostingContext.Configuration["SuryaApplicationInsightsConnectionString"] }, TelemetryConverter.Traces)
             )
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}