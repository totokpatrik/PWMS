using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using PWMS.Presentation.Rest.Middleware;

namespace PWMS.Presentation.Rest.Extensions;
public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseRestPresentation(
        this WebApplication app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();

            // Build a swagger endpoint for each discovered API version
            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });

        app.UseCors("CorsPolicy");

        app.UseMiddleware(typeof(LocalizationMiddleware));
        app.UseMiddleware(typeof(ErrorHandlingMiddleware));

        return app;
    }
}
