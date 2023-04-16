using Azure.Identity;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;

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
                var root = config.Build();
                config.AddEnvironmentVariables();
                var env = context.HostingEnvironment;
                config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
                config.AddAzureKeyVault(new Uri($"{root["Keyvault:Uri"]}"),
       new DefaultAzureCredential(options: new DefaultAzureCredentialOptions
       {
           ExcludeAzurePowerShellCredential = true,
           ExcludeEnvironmentCredential = true,
           ExcludeInteractiveBrowserCredential = true,
           ExcludeSharedTokenCacheCredential = true,
           ExcludeVisualStudioCodeCredential = true,
           ExcludeManagedIdentityCredential = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development,
           ExcludeAzureCliCredential = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development,
           ExcludeVisualStudioCredential = true
       }));
            })
            .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .WriteTo.ApplicationInsights(new TelemetryConfiguration { ConnectionString = hostingContext.Configuration["PersonalWebsiteSuryaApplicationInsightsConnectionString"] }, TelemetryConverter.Traces)
             )
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}