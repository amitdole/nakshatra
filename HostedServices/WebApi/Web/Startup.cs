using Nakshatra.Core.Api.Model.Caching;
using Nakshatra.Core.Services.Caching;
using Serilog;
using Serilog.Enrichers;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.Extensions.Logging.AzureAppServices;
using Nakshatra.HostedServices.Services.Contexts;
using Nakshatra.HostedServices.Services.Queues;
using Nakshatra.HostedServices.Services.Repositories;
using Nakshatra.HostedServices.WebApi.Services.Services;
using Microsoft.EntityFrameworkCore;

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
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.Configure<AzureFileLoggerOptions>(Configuration.GetSection("AzureLogging"));

        services.AddDbContext<ProfileContext>(options =>
                        options.UseCosmos(Configuration["HastaCosmosConnectionString"],
                            "Admin")
                    );
        services.AddScoped<IProfileRepository, ProfileRepository>();

        services.AddScoped<IProfileService, ProfileService>();

        services.AddScoped<IRemindersQueue, RemindersQueue>();

        services.AddScoped<IReminderService, ReminderService>();

        services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
        {
            ConnectionString = Configuration["SuryaApplicationInsightsConnectionString"]
        });

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

        services.AddMvc();
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

        app.UseSerilogRequestLogging();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}