namespace PWMS.Presentation.Rest.Middleware;

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PWMS.Presentation.Rest.Models.Result;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using ILogger = Microsoft.Extensions.Logging.ILogger;

[ExcludeFromCodeCoverage]
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, _logger, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, ILogger log, Exception exception)
    {
        const HttpStatusCode code = HttpStatusCode.InternalServerError;
        var resultDto = new ResultDto<Unit>(Unit.Value, false, new[] { new ErrorDto(exception.Message, code.ToString()) });

        var result = JsonSerializer.Serialize(resultDto);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(result);
    }
}
