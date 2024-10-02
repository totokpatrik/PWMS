using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using PWMS.Api.Infrastructure.Filters;

namespace PWMS.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(HttpGlobalExceptionFilter));
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "PWMS API",
                    Version = "v1",
                    Description = "HTTP API for accessing PWMS data"
                });
            options.DescribeAllParametersInCamelCase();
        });

        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy("Application is running"))
            .AddNpgSql(configuration.GetConnectionString("Database")!);

        return services;
    }
}
