using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using PWMS.Application;
using PWMS.Application.Common.Interfaces;
using PWMS.Infrastructure.Core;
using PWMS.Persistence.PortgreSQL.Extensions;
using PWMS.Presentation.Rest.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true)
    .AddCommandLine(args)
    .AddEnvironmentVariables()
    .Build();

var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services
    .AddLogging(configuration)
    .AddOptions()
    .AddNgpSqlPersistence(configuration)
    .AddAuth(configuration)
    .AddApplication()
    .AddCoreInfrastructure()
    .AddRestPresentation(configuration, builder.Environment)
    .AddHealthChecks();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(builder => builder
        .AddService(
            serviceName: Assembly.GetExecutingAssembly().GetName().Name!,
            serviceVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
            serviceInstanceId: Environment.MachineName))
    .WithTracing(builder => builder
        .AddAspNetCoreInstrumentation(options =>
{
    options.RecordException = true;
})
        .AddRestOpenTelemetry()
        .AddNgpSqlPersistenceOpenTelemetry()
        .AddOtlpExporter()
        .AddConsoleExporter()
    );

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        await context.MigrateAsync();
        await context.SeedAsync();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

app.UseRestPresentation(environment)
    .UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRestEndpoints();

app.MapHealthChecks("/health/startup");
app.MapHealthChecks("/healthz", new HealthCheckOptions { Predicate = _ => false });
app.MapHealthChecks("/ready", new HealthCheckOptions { Predicate = _ => false });

app.MapHealthChecks("/health/info", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

//We need public access to the class for tests
public partial class Program { }