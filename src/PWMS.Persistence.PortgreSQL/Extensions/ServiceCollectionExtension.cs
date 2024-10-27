namespace PWMS.Persistence.PortgreSQL.Extensions;

using Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PWMS.Application.Common.Interfaces;
using PWMS.Common.Extensions;
using PWMS.Domain.Auth.Entities;
using PWMS.Persistence.PortgreSQL.Data;
using Serilog;
using System.Text;

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

        ConfigureDbContextFactory(services, configuration, optionsBuilder);

        services.TryAddScoped<ApplicationDbContextFactory>();

        services.TryAddScoped<IApplicationDbContext>(p =>
            p.GetRequiredService<ApplicationDbContextFactory>().CreateDbContext());

        services.TryAddScoped<ApplicationDbContext>(p =>
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

    public static IServiceCollection AddAuth(this IServiceCollection services,
    IConfiguration configuration)
    {
        var jwtConfiguration = configuration.GetSection(JwtConfigurationSection.SectionName).Get<JwtDetails>();
        ArgumentNullException.ThrowIfNull(jwtConfiguration);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.IncludeErrorDetails = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = false,
                ValidAudience = jwtConfiguration.Audience,
                ValidIssuer = jwtConfiguration.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfiguration.Secret))
            };
        });

        services.AddAuthorizationBuilder();

        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddTokenProvider<DataProtectorTokenProvider<User>>(jwtConfiguration.Provider);

        services.AddAuthorization();
        services.TryAddScoped<IDbInitializer, DbInitializer>();

        // maybe load from config also
        services.Configure<DataProtectionTokenProviderOptions>(opt =>
        {
            opt.TokenLifespan = TimeSpan.FromHours(1);
        });

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
