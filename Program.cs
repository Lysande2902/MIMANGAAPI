using Microsoft.EntityFrameworkCore;
using MiMangaBot.Infrastructure;
using MiMangaBot.Infrastructure.Database;
using MiMangaBot.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión a Railway
var connectionString =
    "Server=crossover.proxy.rlwy.net;Port=47368;Database=railway;Uid=root;Pwd=lNJhARzfrNWndzwpJdJIhgAfPWjgmuWa;";

// EF Core con MySQL
builder.Services.AddDbContext<MangaDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Servicios y repositorios
builder.Services.AddScoped<MangaServices>();
builder.Services.AddScoped<MangaRepository>();

// Controladores y documentación
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
