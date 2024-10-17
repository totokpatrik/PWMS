using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PWMS.Application.Common.Interfaces;
using PWMS.Presentation.Rest.Filters;
using System.Reflection;

namespace PWMS.Presentation.Rest.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRestPresentation(
        this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var corsParams = configuration.GetSection("Cors").Get<List<string>>();

        ArgumentNullException.ThrowIfNull(corsParams);

        services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
        {
            builder.WithOrigins(corsParams.ToArray())
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }));

        services.AddHttpContextAccessor()
            .AddSwagger(configuration, Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssemblyContaining<IApplicationDbContext>(ServiceLifetime.Scoped, null, true)
            .AddControllers(options => options.Filters.Add<CustomExceptionFilterAttribute>())
            .AddApplicationPart(Assembly.GetExecutingAssembly());

        return services;
    }
}
