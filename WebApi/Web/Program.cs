using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contexts;
using Services.Repositories;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Configuration.AddAzureKeyVault(
       new Uri($"{builder.Configuration["Keyvault:Uri"]}"),
       new DefaultAzureCredential());

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