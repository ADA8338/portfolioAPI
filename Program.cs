using Microsoft.EntityFrameworkCore;
using PortfolioAPI;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Read DATABASE_URL from Render
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrEmpty(databaseUrl))
{
    throw new Exception("DATABASE_URL environment variable is not set.");
}

// 🔹 Convert Render Postgres URL to Npgsql format
var uri = new Uri(databaseUrl);
var userInfo = uri.UserInfo.Split(':');

var connectionString =
    $"Host={uri.Host};" +
    $"Port={uri.Port};" +
    $"Database={uri.AbsolutePath.Trim('/')};" +
    $"Username={userInfo[0]};" +
    $"Password={userInfo[1]};" +
    $"SSL Mode=Require;Trust Server Certificate=true";

// 🔹 Register DbContext
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseNpgsql(connectionString));

// 🔹 Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Swagger only for API testing (safe in Render)
app.UseSwagger();
app.UseSwaggerUI();

// 🔹 Routing
app.UseAuthorization();
app.MapControllers();

// 🔹 Health check
app.MapGet("/health", () => new
{
    service = "Portfolio API",
    status = "Running",
    environment = app.Environment.EnvironmentName
});

app.Run();
