using Azure.Identity;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Services.Contexts;
using Services.Repositories;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Configuration.AddAzureKeyVault(
       new Uri($"{builder.Configuration["Keyvault:Uri"]}"),
       new DefaultAzureCredential(true));

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
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();