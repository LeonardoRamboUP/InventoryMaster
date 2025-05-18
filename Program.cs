using Microsoft.EntityFrameworkCore;
using InventoryMaster.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InventoryMaster", Version = "v1" });
});

// Configura o DbContext com MySQL
const string connectionStringKey = "MySQL";
var connectionString = builder.Configuration.GetConnectionString(connectionStringKey);
if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("A string de conexão 'MySQL' está ausente ou vazia. Verifique o arquivo 'appsettings.json'.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);

// Configura CORS para permitir qualquer origem
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configura autenticação JWT
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
    throw new InvalidOperationException("A chave JWT está ausente. Defina 'Jwt:Key' no appsettings.json.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();