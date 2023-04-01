using Azure.Identity;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Nakshatra.Core.Api.Model.Caching;
using Nakshatra.Core.Services.Caching;
using Nakshatra.HostedServices.Services.Contexts;
using Nakshatra.HostedServices.Services.Queues;
using Nakshatra.HostedServices.Services.Repositories;
using Nakshatra.HostedServices.Services.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Configuration.AddAzureKeyVault(
       new Uri($"{builder.Configuration["Keyvault:Uri"]}"),
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

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProfileContext>(options =>
                options.UseCosmos(builder.Configuration["HastaCosmosConnectionString"],
                    "Admin")
            );
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

builder.Services.AddScoped<IProfileService, ProfileService>();

builder.Services.AddScoped<IRemindersQueue, RemindersQueue>();

builder.Services.AddScoped<IReminderService, ReminderService>();

builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
});

builder.Services.Configure<CacheConfiguration>(builder.Configuration.GetSection("CacheConfiguration"));

builder.Services.AddMemoryCache();

if (builder.Configuration["CacheProvider"].Equals("memory", StringComparison.InvariantCultureIgnoreCase))
{
    builder.Services.AddTransient<ICacheService, MemoryCacheService>();
}
else
{
    builder.Services.AddTransient<ICacheService, RedisCacheService>();
}
//builder.Services.AddTransient<Func<CacheType, ICacheService>>(serviceProvider => key =>
//{
//    switch (key)
//    {
//        case CacheType.Memory:
//            return serviceProvider.GetService<MemoryCacheService>();
//        default:
//            return serviceProvider.GetService<RedisCacheService>();
//    }
//});


var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (environment != Environments.Production)
{
    app.UseDeveloperExceptionPage();
}
app.UseAuthorization();

app.MapControllers();

app.Run();