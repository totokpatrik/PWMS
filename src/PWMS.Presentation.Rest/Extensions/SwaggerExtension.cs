using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PWMS.Presentation.Rest.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace PWMS.Presentation.Rest.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwagger(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly executingAssembly)
    {
        services
            .AddEndpointsApiExplorer()
            .AddApiVersioning(versioningOptions =>
            {
                versioningOptions.DefaultApiVersion = new ApiVersion(1.0);
                versioningOptions.ReportApiVersions = true;
                versioningOptions.AssumeDefaultVersionWhenUnspecified = true;
            })
            .AddApiExplorer(explorerOptions =>
            {
                explorerOptions.GroupNameFormat = "'v'VVV";
                explorerOptions.SubstituteApiVersionInUrl = true;
            });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            };

            options.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };

            options.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }
}
