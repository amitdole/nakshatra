using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nakshatra.Core.Api.Model.Caching;
using Nakshatra.Core.Services.Caching;
using Nakshatra.HostedServices.Services.Contexts;
using Nakshatra.HostedServices.WebApi.Web.Controllers;

namespace Nakshatra.HostedServices.WebApi.Tests
{
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
                 reloadOnChange: true)
            .AddAzureKeyVault(
               new Uri("https://kv-nakshatra-test-001.vault.azure.net/"),
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
               })).Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);

            serviceCollection.Configure<CacheConfiguration>(configuration.GetSection("CacheConfiguration"));
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<ICacheService, MemoryCacheService>();

            var profileConnectionstring = configuration["HastaCosmosConnectionString"] ?? null;
            var profileDatabase = "Admin";

            if (profileConnectionstring != null)
            {
                _ = serviceCollection.AddDbContext<ProfileContext>(options =>
                    options.UseCosmos(profileConnectionstring, profileDatabase)
                );
            }

            serviceCollection.AddSingleton<Services.Services.IProfileService, Services.Services.ProfileService>();
            serviceCollection.AddSingleton<Services.Repositories.IProfileRepository, Services.Repositories.ProfileRepository>();
            serviceCollection.AddSingleton<ProfileContext>();
            serviceCollection.AddSingleton<ProfileController, ProfileController>();

            serviceCollection.AddTransient<RedisCacheService>();

            var cacheProvider = configuration["CacheProvider"] ?? null;
            if (cacheProvider == null || cacheProvider.Equals("memory", StringComparison.OrdinalIgnoreCase))
            {
                serviceCollection.AddTransient<ICacheService, MemoryCacheService>();
            }
            else
            {
                serviceCollection.AddTransient<ICacheService, RedisCacheService>();
            }

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}