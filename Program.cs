using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// ---------- DATABASE ----------
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

builder.Services.AddDbContext<PortfolioDbContext>(options =>
{
    options.UseNpgsql(databaseUrl);
});

// ---------- SERVICES ----------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ---------- MIDDLEWARE ----------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ---------- HEALTH ----------
app.MapGet("/", () => new
{
    service = "Portfolio API",
    status = "Running",
    environment = app.Environment.EnvironmentName
});

app.MapGet("/api/health", () => Results.Ok("Healthy"));

// ---------- API ENDPOINTS ----------
app.MapGet("/api/projects", async (PortfolioDbContext db) =>
    await db.Projects.OrderByDescending(p => p.CreatedAt).ToListAsync()
);

app.MapPost("/api/projects", async (PortfolioDbContext db, PortfolioAPI.Models.Project project) =>
{
    db.Projects.Add(project);
    await db.SaveChangesAsync();
    return Results.Created($"/api/projects/{project.Id}", project);
});

app.Run();
