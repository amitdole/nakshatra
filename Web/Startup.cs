using API.Repositories;
using DataServices;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.Extensions.Options;
using API.Model.Caching;
using Services.CacheService;
using Services.Profile;
using API.Model.Service;
using API.Model.Profile;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();

        services.AddApplicationInsightsTelemetry();

        services.AddSingleton<IJavaScriptSnippet, JavaScriptSnippet>();

        // Add 'JavaScriptSnippet' "Service" for backwards compatibility. To remove in favour of 'IJavaScriptSnippet'. 
        services.AddSingleton<JavaScriptSnippet>();

        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IProfileService, ProfileService>();

        services.Configure<CacheConfiguration>(Configuration.GetSection("CacheConfiguration"));

        services.AddMemoryCache();
        services.AddTransient<MemoryCacheService>();
        services.AddTransient<Func<CacheType, ICacheService>>(serviceProvider => key =>
        {
            switch (key)
            {
                case CacheType.Memory:
                    return serviceProvider.GetService<MemoryCacheService>();
                default:
                    return serviceProvider.GetService<MemoryCacheService>();
            }
        });

        //SecretClientOptions options = new SecretClientOptions()
        //{
        //    Retry =
        //{
        //    Delay= TimeSpan.FromSeconds(2),
        //    MaxDelay = TimeSpan.FromSeconds(16),
        //    MaxRetries = 5,
        //    Mode = RetryMode.Exponential
        // }
        //};
        //var client = new SecretClient(new Uri(Configuration["Keyvault:Uri"]), new DefaultAzureCredential(), options);

        var configDictonary = new Dictionary<string, string>();
        //configDictonary.Add("UserProfiles", client.GetSecret("UserProfiles").Value.Value);
        //configDictonary.Add("SendGridAPISecretKey", client.GetSecret("SendGridAPISecretKey").Value.Value);

        configDictonary.Add("UserProfiles", Configuration["UserProfiles"]);
        configDictonary.Add("SendGridAPISecretKey", Configuration["SendGridAPISecretKey"]);
        Action<Configuration> configuration = (opt =>
        {
            opt.Metadata = configDictonary;
        });
        services.Configure(configuration);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<Configuration>>().Value);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
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

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapDefaultControllerRoute();
        });
    }
}