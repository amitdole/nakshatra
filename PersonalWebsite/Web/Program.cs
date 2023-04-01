using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var root = config.Build();

                config.AddAzureKeyVault(new Uri($"{root["Keyvault:Uri"]}"),
       new DefaultAzureCredential(options: new DefaultAzureCredentialOptions
       {
           ExcludeAzurePowerShellCredential = true,
           ExcludeEnvironmentCredential = true,
           ExcludeInteractiveBrowserCredential = true,
           ExcludeSharedTokenCacheCredential = true,
           ExcludeVisualStudioCodeCredential = true,
           ExcludeManagedIdentityCredential = false,
           ExcludeAzureCliCredential = false,
           ExcludeVisualStudioCredential = false
       }));
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}