using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using PWMS.Common.Extensions;
using Serilog;

namespace PWMS.Presentation.Rest.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CustomExceptionFilterAttribute(IWebHostEnvironment webHostEnvironment) =>
        _webHostEnvironment = webHostEnvironment.ThrowIfNull(nameof(webHostEnvironment));

    public override void OnException(ExceptionContext context)
    {
        context.HttpContext.Response.ContentType = "application/json";

        var (actionResult, statusCode) = ResultFactory.CreatedResult(context);
        context.HttpContext.Response.StatusCode = statusCode;
        context.Result = actionResult;
    }
}
