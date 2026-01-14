using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ================= DATABASE =================
// Works locally + on Render
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? builder.Configuration["DATABASE_URL"];

if (!string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<PortfolioDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    Console.WriteLine("⚠️ Database connection string not found");
}

var app = builder.Build();

// ================= MIDDLEWARE =================
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
