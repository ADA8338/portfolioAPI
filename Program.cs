using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Add Services
// --------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------------
// PostgreSQL Database
// --------------------
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

// --------------------
// Middleware
// --------------------
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
