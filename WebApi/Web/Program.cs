using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Contexts;
using Services.Repositories;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();



//var options = new SecretClientOptions()
//{
//    Retry =
//        {
//            Delay= TimeSpan.FromSeconds(2),
//            MaxDelay = TimeSpan.FromSeconds(16),
//            MaxRetries = 5,
//            Mode = RetryMode.Exponential
//         }
//};
//var client = new SecretClient(new Uri("https://nakshatrakeyvault.vault.azure.net/"), new DefaultAzureCredential(), options);

ClientSecretCredential csc = new(builder.Configuration["Keyvault:TenentId"], builder.Configuration["Keyvault:ClientId"], builder.Configuration["Keyvault:ClientSecret"]);
builder.Configuration.AddAzureKeyVault(
    new Uri(builder.Configuration["Keyvault:Uri"]), csc);

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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();