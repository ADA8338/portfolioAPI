using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------
// DATABASE CONNECTION
// --------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new Exception("❌ DefaultConnection string is missing");
}

builder.Services.AddDbContext<PortfolioDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// --------------------------------------------------
// SERVICES
// --------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------------------------------------------
// BUILD APP
// --------------------------------------------------
var app = builder.Build();

// --------------------------------------------------
// AUTO CREATE DATABASE (IMPORTANT FOR RENDER)
// --------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
    db.Database.EnsureCreated();
}

// --------------------------------------------------
// MIDDLEWARE
// --------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
