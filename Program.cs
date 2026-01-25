using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICES --------------------

// Database (PostgreSQL)
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (safe default – frontend friendly)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// -------------------- APP --------------------

var app = builder.Build();

// Apply EF Core migrations safely (NO EnsureCreated)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
    db.Database.Migrate();
}

// Swagger (keep enabled in prod for API testing)
app.UseSwagger();
app.UseSwaggerUI();

// Middlewares
app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

// Map controllers
app.MapControllers();

// Port binding for Render
app.Run();
