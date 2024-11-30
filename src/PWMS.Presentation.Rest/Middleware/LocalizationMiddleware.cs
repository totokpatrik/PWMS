using System.Globalization;

namespace PWMS.Presentation.Rest.Middleware;

public class LocalizationMiddleware
{
    private readonly RequestDelegate _next;
    public LocalizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext httpContext)
    {
        var culture = httpContext.Request.Headers["Accept-Language"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();
        if (!string.IsNullOrEmpty(culture))
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }

        await _next(httpContext);
    }
}
