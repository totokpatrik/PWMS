using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PWMS.Api.Extensions;
using PWMS.Core.AppSettings;
using PWMS.Core.Extensions;
using PWMS.Infrastructure.Data.Context;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace PWMS.Api.Extensions;

[ExcludeFromCodeCoverage]
internal static class ServicesCollectionExtensions
{
    private const int DbMaxRetryCount = 3;
    private const int DbCommandTimeout = 35;
    private const string DbMigrationAssemblyName = "PWMS.Api";

    private static readonly string[] DbRelationalTags = ["database", "ef-core", "sql-server", "relational"];
    private static readonly string[] DbNoSqlTags = ["database", "mongodb", "no-sql"];

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swaggerOptions =>
        {
            swaggerOptions.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Shop (e-commerce)",
                Description = "ASP.NET Core C# CQRS Event Sourcing, REST API, DDD, SOLID Principles and Clean Architecture",
                Contact = new OpenApiContact
                {
                    Name = "Jean Gatto",
                    Email = "jean_gatto@hotmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License"
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            swaggerOptions.IncludeXmlComments(xmlPath, true);
        });
    }

    public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<ConnectionOptions>();

        var healthCheckBuilder = services
            .AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>(tags: DbRelationalTags);
    }

    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
            ConfigureDbContext<ApplicationDbContext>(serviceProvider, optionsBuilder, QueryTrackingBehavior.TrackAll));

        services.AddDbContext<EventStoreDbContext>((serviceProvider, optionsBuilder) =>
            ConfigureDbContext<EventStoreDbContext>(serviceProvider, optionsBuilder, QueryTrackingBehavior.NoTrackingWithIdentityResolution));

        return services;
    }

    private static void ConfigureDbContext<TContext>(
        IServiceProvider serviceProvider,
        DbContextOptionsBuilder optionsBuilder,
        QueryTrackingBehavior queryTrackingBehavior) where TContext : DbContext
    {
        var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
        var options = serviceProvider.GetOptions<ConnectionOptions>();
        var environment = serviceProvider.GetRequiredService<IHostEnvironment>();
        var envIsDevelopment = environment.IsDevelopment();

        optionsBuilder
            .UseNpgsql(options.Database, options =>
            {
                options
                    .MigrationsAssembly(DbMigrationAssemblyName)
                    .EnableRetryOnFailure(DbMaxRetryCount)
                    .CommandTimeout(DbCommandTimeout);
            })
            .EnableDetailedErrors(envIsDevelopment)
            .EnableSensitiveDataLogging(envIsDevelopment)
            .UseQueryTrackingBehavior(queryTrackingBehavior)
            .LogTo((eventId, _) => eventId.Id == CoreEventId.ExecutionStrategyRetrying, eventData =>
            {
                if (eventData is not ExecutionStrategyEventData retryEventData)
                    return;

                var exceptions = retryEventData.ExceptionsEncountered;

                logger.LogWarning(
                    "DbContext: Retry #{Count} with delay {Delay} due to error: {Message}",
                    exceptions.Count,
                    retryEventData.Delay,
                    exceptions[^1].Message);
            });
    }
}