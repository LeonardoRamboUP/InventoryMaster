using Microsoft.EntityFrameworkCore;
using InventoryMaster.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with MySQL
const string connectionStringKey = "MySQL";
var connectionString = builder.Configuration.GetConnectionString(connectionStringKey);
if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("The connection string for 'MySQL' is missing or empty. Please check your configuration in 'appsettings.json'.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Check if HTTPS is enabled in the environment before using HTTPS redirection
if (app.Environment.IsProduction() || app.Environment.IsStaging())
{
    app.UseHttpsRedirection();
}

// Endpoint raiz
app.MapGet("/", () => "InventoryMaster está rodando!");

app.Run();