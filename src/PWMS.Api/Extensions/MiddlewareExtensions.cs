using Microsoft.AspNetCore.Builder;
using PWMS.Api.Middlewares;

namespace PWMS.Api.Extensions;

internal static class MiddlewareExtensions
{
    public static void UseErrorHandling(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ErrorHandlingMiddleware>();
}
