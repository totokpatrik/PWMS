using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PWMS.Presentation.Rest.Swagger;
using Swashbuckle.AspNetCore.Filters;
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
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the bearer scheme, e.g. \"bearer {token} \"",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.OperationFilter<SecurityRequirementsOperationFilter>();

            var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
            c.IncludeXmlComments(filePath);
            c.CustomSchemaIds(type => type.ToString());
        });

        return services;
    }
}
