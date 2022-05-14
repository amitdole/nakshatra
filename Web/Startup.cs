using API.Model;
using API.Repositories;
using DataServices;
using Microsoft.ApplicationInsights.AspNetCore;
using Services;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using Microsoft.Extensions.Options;

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