using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Read DATABASE_URL
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrWhiteSpace(databaseUrl))
{
    throw new Exception("DATABASE_URL environment variable is not set.");
}

// 🔹 Parse DATABASE_URL safely
var uri = new Uri(databaseUrl);
var userInfo = uri.UserInfo.Split(':');

// 🔹 FIX: Handle missing port
var port = uri.Port > 0 ? uri.Port : 5432;

// 🔹 Build Npgsql connection string
var connectionString =
    $"Host={uri.Host};" +
    $"Port={port};" +
    $"Database={uri.AbsolutePath.Trim('/')};" +
    $"Username={userInfo[0]};" +
    $"Password={userInfo[1]};" +
    $"SSL Mode=Require;Trust Server Certificate=true";

// 🔹 Register DbContext
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseNpgsql(connectionString)
);

var app = builder.Build();

// 🔹 Auto-create tables
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
    db.Database.EnsureCreated();
}

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
