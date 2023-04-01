using DataServices;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.Extensions.Options;
using Services.Profile;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web.UI;
using Nakshatra.Api.Repositories;
using Nakshatra.Api.Model.Service;
using Nakshatra.Core.Api.Model.Caching;
using Nakshatra.Core.Services.Caching;
using System.Configuration;
using Nakshatra.Api.Model.Profile;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMicrosoftIdentityWebAppAuthentication(Configuration);

        services.AddControllersWithViews();

        //services.AddDbContext<WebContext>(options =>
        //        options.UseSqlServer(Configuration.GetConnectionString("WebContextConnection")));

        //services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
        //    .AddEntityFrameworkStores<WebContext>();

        services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizePage("/reminders");
        })
            .AddMvcOptions(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters
                .Add(new AuthorizeFilter(policy));
            }).AddMicrosoftIdentityUI();


        services.AddApplicationInsightsTelemetry();

        services.AddSingleton<IJavaScriptSnippet, JavaScriptSnippet>();

        // Add 'JavaScriptSnippet' "Service" for backwards compatibility. To remove in favour of 'IJavaScriptSnippet'. 
        services.AddSingleton<JavaScriptSnippet>();

        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IUserProfileService, UserProfileService>();

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

        var configDictonary = new Dictionary<string, string>();
        configDictonary.Add("UserProfiles", Configuration["UserProfiles"]);
        configDictonary.Add("SendGridAPISecretKey", Configuration["SendGridAPISecretKey"]);

        Action<ExtendedAttributes> configuration = (opt =>
        {
            opt.Metadata = configDictonary;
        });

        services.Configure(configuration);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<Configuration>>().Value);
    }

    private static string GetAuthenticationScheme()
    {
        return CookieAuthenticationDefaults.AuthenticationScheme;
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

        app.UseAuthentication();
        app.UseAuthorization();

       

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapDefaultControllerRoute();
        });
    }
}