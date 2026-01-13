using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

#region LOGGING
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
#endregion

#region SERVICES
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
#endregion

var app = builder.Build();

#region MIDDLEWARE

// Global request logging
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

    logger.LogInformation(
        "Incoming request: {Method} {Path}",
        context.Request.Method,
        context.Request.Path
    );

    await next();

    logger.LogInformation(
        "Response status: {StatusCode}",
        context.Response.StatusCode
    );
});

app.UseRouting();
app.UseAuthorization();

#endregion

#region ENDPOINTS
app.MapControllers();

// Root endpoint (optional)
app.MapGet("/", () => Results.Ok(new
{
    service = "Portfolio API",
    status = "Running",
    environment = app.Environment.EnvironmentName
}));
#endregion

app.Run();
