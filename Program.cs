var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------

builder.Services.AddControllers();

// Swagger (simple & stable)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS for GitHub Pages
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGithub", policy =>
    {
        policy
            .WithOrigins("https://ada8338.github.io")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// --------------------
// Middleware
// --------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// OPTIONAL: comment if HTTPS warning appears
// app.UseHttpsRedirection();

app.UseCors("AllowGithub");

app.MapControllers();

// Root endpoint (avoid 404)
app.MapGet("/", () => "Portfolio API is running 🚀");

app.Run();
