using DataServices;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.Extensions.Options;
using Services.Profile;
using Nakshatra.Api.Repositories;
using Nakshatra.Api.Model.Service;
using Nakshatra.Core.Api.Model.Caching;
using Nakshatra.Core.Services.Caching;
using System.Configuration;
using Nakshatra.Api.Model.Profile;
using Nakshatra.Core.Services.Email;
using Nakshatra.Services.Profile;
using Serilog;
using Serilog.Enrichers;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;

        Log.Logger = new LoggerConfiguration()
         .ReadFrom.Configuration(configuration)
         .Enrich.With(
          new MachineNameEnricher(),
          new ThreadIdEnricher()
        ).CreateLogger();
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddRazorPages();

        services.AddApplicationInsightsTelemetry();


        services.AddSingleton<IJavaScriptSnippet, JavaScriptSnippet>();

        // Add 'JavaScriptSnippet' "Service" for backwards compatibility. To remove in favour of 'IJavaScriptSnippet'. 
        services.AddSingleton<JavaScriptSnippet>();

        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IUserProfileService, UserProfileService>();

        services.Configure<CacheConfiguration>(Configuration.GetSection("CacheConfiguration"));

        services.AddMemoryCache();

        if (Configuration["CacheProvider"].Equals("memory", StringComparison.InvariantCultureIgnoreCase))
        {
            services.AddTransient<ICacheService, MemoryCacheService>();
        }
        else
        {
            services.AddTransient<ICacheService, RedisCacheService>();
        }

        services.AddSingleton<IEmailService, EmailService>();

        var configDictonary = new Dictionary<string, string>();

        Action<ExtendedAttributes> configuration = (opt =>
        {
            opt.Metadata = configDictonary;
        });

        services.Configure(configuration);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<Configuration>>().Value);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsProduction())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapDefaultControllerRoute();
        });
    }
}