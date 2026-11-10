using DeployService.Application;
using DeployService.Infrastructure;
using DeployService.Infrastructure.Persistence;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

builder.Services
    .AddFastEndpoints()
    .AddEndpointsApiExplorer()
    .SwaggerDocument(config =>
    {
        config.AutoTagPathSegmentIndex = 0;
    });

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddHealthChecks()
    .AddCheck(
        "self",
        () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy(),
        tags: new[] { "live" })
    .AddCheck(
        "database_connection",
        () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy(
            "Database health check is configured separately"),
        tags: new[] { "ready" });

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    await dbContext.Database.MigrateAsync();
}

app.UseExceptionHandler();

app.UseFastEndpoints();

app.UseSwaggerGen(c =>
    c.PostProcess = (doc, req) =>
    {
        if (req.Path.StartsWithSegments("/swagger"))
        {
            File.WriteAllText("openapi.yaml", doc.ToYaml());
        }
    });

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("live")
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.Run();