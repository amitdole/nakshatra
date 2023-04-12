using Azure.Identity;
using BenchmarkDotNet.Configs;
using DataServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nakshatra.Api.Model.Profile;
using Nakshatra.Api.Model.Service;
using Nakshatra.Api.Repositories;
using Nakshatra.Core.Api.Model.Caching;
using Nakshatra.Core.Services.Caching;
using Nakshatra.HostedServices.Services.Contexts;
using Services.Blogger;
using Services.Profile;

namespace Nakshatra.HostedServices.Services.Tests;

public class TestBase
{
    public TestBase()
    {
        TestHost = CreateHostBuilder().Build();
        Task.Run(() => TestHost.RunAsync());
    }

    public IHost TestHost { get; }

    public static IHostBuilder CreateHostBuilder(string[] args = null) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddAzureKeyVault(
                   new Uri("https://kv-nakshatra-test-001.vault.azure.net/"),
                   new DefaultAzureCredential(options: new DefaultAzureCredentialOptions
                   {
                       ExcludeAzurePowerShellCredential = true,
                       ExcludeEnvironmentCredential = true,
                       ExcludeInteractiveBrowserCredential = true,
                       ExcludeSharedTokenCacheCredential = true,
                       ExcludeVisualStudioCodeCredential = true,
                       ExcludeManagedIdentityCredential = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development,
                       ExcludeAzureCliCredential = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != Environments.Development,
                       ExcludeVisualStudioCredential = true
                   }));
                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            })
            .ConfigureServices((hostContext, services) =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var config = serviceProvider.GetService<IConfiguration>();

                services.AddOptions();
                services.AddSingleton<IConfiguration>(config);

                services.Configure<CacheConfiguration>(config.GetSection("CacheConfiguration"));

                services.AddMemoryCache();
                services.AddSingleton<ICacheService, MemoryCacheService>();

                var profileConnectionstring = config["HastaCosmosConnectionString"] ?? null;
                var profileDatabase = "Admin";

                if (profileConnectionstring != null)
                {
                    _ = services.AddDbContext<ProfileContext>(options =>
                        options.UseCosmos(profileConnectionstring, profileDatabase)
                    );
                }

                services.AddSingleton<IUserProfileRepository, UserProfileRepository>();
                services.AddSingleton<IUserProfileService, UserProfileService>();

                services.AddSingleton<Blog>();
                services.AddSingleton<IBlogService, BloggerService>();

                services.AddTransient<RedisCacheService>();

                var cacheProvider = config["CacheProvider"] ?? null;
                if (cacheProvider == null || cacheProvider.Equals("memory", StringComparison.OrdinalIgnoreCase))
                {
                    services.AddTransient<ICacheService, MemoryCacheService>();
                }
                else
                {
                    services.AddTransient<ICacheService, RedisCacheService>();
                }
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                //logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                //logging.AddConsole();
            });

    //public ServiceProvider ServiceProvider { get; private set; }
    //public TestBase()
    //{
    //    var serviceCollection = new ServiceCollection();

    //    var configuration =  new ConfigurationBuilder()
    //     .SetBasePath(Directory.GetCurrentDirectory())
    //     .AddJsonFile(
    //          path: "appsettings.json",
    //          optional: false,
    //          reloadOnChange: true)
    //    .AddAzureKeyVault(
    //       new Uri("https://kv-nakshatra-test-001.vault.azure.net/"),
    //       new DefaultAzureCredential(options: new DefaultAzureCredentialOptions
    //       {
    //           ExcludeAzurePowerShellCredential = true,
    //           ExcludeEnvironmentCredential = true,
    //           ExcludeInteractiveBrowserCredential = true,
    //           ExcludeSharedTokenCacheCredential = true,
    //           ExcludeVisualStudioCodeCredential = true,
    //           ExcludeManagedIdentityCredential = false,
    //           ExcludeAzureCliCredential = false,
    //           ExcludeVisualStudioCredential = false
    //       })).Build();



    //    serviceCollection.AddSingleton<IConfiguration>(configuration);

    //    serviceCollection.Configure<CacheConfiguration>(configuration.GetSection("CacheConfiguration"));

    //    serviceCollection.AddMemoryCache();
    //    serviceCollection.AddSingleton<ICacheService, MemoryCacheService>();

    //    var profileConnectionstring = configuration["HastaCosmosConnectionString"] ?? null;
    //    var profileDatabase = "Admin";

    //    if (profileConnectionstring != null)
    //    {
    //        _ = serviceCollection.AddDbContext<ProfileContext>(options =>
    //            options.UseCosmos(profileConnectionstring, profileDatabase)
    //        );
    //    }

    //    serviceCollection.AddScoped<IUserProfileRepository, UserProfileRepository>();
    //    serviceCollection.AddScoped<IUserProfileService, UserProfileService>();

    //    serviceCollection.AddSingleton<IBlogService, BloggerService>();
    //    //serviceCollection.AddSingleton<Services.Repositories.IProfileRepository, Services.Repositories.ProfileRepository>();
    //    //serviceCollection.AddSingleton<ProfileContext>();
    //    //serviceCollection.AddSingleton<ProfileController, ProfileController>();

    //    serviceCollection.AddTransient<RedisCacheService>();

    //    var cacheProvider = configuration["CacheProvider"] ?? null;
    //    if (cacheProvider == null || cacheProvider.Equals("memory", StringComparison.OrdinalIgnoreCase))
    //    {
    //        serviceCollection.AddTransient<ICacheService, MemoryCacheService>();
    //    }
    //    else
    //    {
    //        serviceCollection.AddTransient<ICacheService, RedisCacheService>();
    //    }

    //    ServiceProvider = serviceCollection.BuildServiceProvider();
    //}
}