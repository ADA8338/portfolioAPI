using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// ==============================
// Database configuration
// ==============================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// If running on Render, use DATABASE_URL
if (string.IsNullOrEmpty(connectionString))
{
    var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

    if (!string.IsNullOrEmpty(databaseUrl))
    {
        // Convert DATABASE_URL to Npgsql format
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');

        connectionString =
            $"Host={uri.Host};" +
            $"Port={uri.Port};" +
            $"Database={uri.AbsolutePath.Trim('/')};" +
            $"Username={userInfo[0]};" +
            $"Password={userInfo[1]};" +
            $"SSL Mode=Require;Trust Server Certificate=true";
    }
}

// Register DbContext
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// ==============================
// Services
// ==============================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ==============================
// Middleware
// ==============================
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// ==============================
// Health check endpoint
// ==============================
app.MapGet("/", () => new
{
    service = "Portfolio API",
    status = "Running",
    environment = app.Environment.EnvironmentName
});

app.Run();
