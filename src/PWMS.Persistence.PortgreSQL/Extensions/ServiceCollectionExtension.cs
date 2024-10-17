namespace PWMS.Persistence.PortgreSQL.Extensions;

using Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PWMS.Application.Common.Interfaces;
using PWMS.Common.Extensions;
using PWMS.Persistence.PortgreSQL.Data;
using Serilog;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Add PostgresSQL as a persistence layer
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the MassTransits to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing settings to be used.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddNgpSqlPersistence(this IServiceCollection services,
        IConfiguration configuration,
        Action<IServiceProvider, DbContextOptionsBuilder>? optionsBuilder = null)
    {
        configuration.ThrowIfNull(nameof(configuration));

        services.AddScoped<IValidator<PostgresConnection>, PostgresConnectionValidator>();

        ConfigureDbContextFactory(services, configuration, optionsBuilder);

        services.TryAddScoped<IDbInitializer, DbInitializer>();
        services.TryAddScoped<ApplicationDbContextFactory>();

        services.TryAddScoped<IApplicationDbContext>(p =>
            p.GetRequiredService<ApplicationDbContextFactory>().CreateDbContext());

        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableTo(typeof(RepositoryBase<>)))
            .AsMatchingInterface()
            .WithScopedLifetime());

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        return services;
    }

    private static IServiceCollection ConfigureDbContextFactory(this IServiceCollection services,
        IConfiguration configuration,
        Action<IServiceProvider, DbContextOptionsBuilder>? optionsBuilder = null)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var currentConfiguration = configuration.GetSection(DbConfigurationSection.SectionName).Get<PostgresConnection>();
        ArgumentNullException.ThrowIfNull(currentConfiguration);

        services.AddEntityFrameworkNpgsql()
            .AddPooledDbContextFactory<ApplicationDbContext>(optionsAction: (provider, options) =>
            {
                optionsBuilder?.Invoke(provider, options);


                options.UseInternalServiceProvider(provider);
                options.UseNpgsql(currentConfiguration.ConnectionString);

                if (currentConfiguration.LoggingEnabled)
                {
                    options.EnableDbLogging();
                }
            });

        if (currentConfiguration.HealthCheckEnabled)
        {
            services.AddHealthChecks().AddNpgSql(currentConfiguration.ConnectionString);
        }

        return services;
    }

    private static DbContextOptionsBuilder EnableDbLogging(this DbContextOptionsBuilder builder) => builder
            .LogTo(
                msg => Log.Logger.Information(msg),
                new[] { DbLoggerCategory.Database.Name })
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
}
