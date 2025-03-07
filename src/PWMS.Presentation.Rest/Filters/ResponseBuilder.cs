using Microsoft.AspNetCore.Mvc.Filters;
using PWMS.Application.Common.Exceptions;
using PWMS.Presentation.Rest.Filters.Results;
using PWMS.Presentation.Rest.Models.Result;
using System.Net;

namespace PWMS.Presentation.Rest.Filters;
public static class ResultFactory
{
    public static (IActionResult?, int) CreatedResult(ExceptionContext context)
    {
        var errorMessage = context.Exception.Message;

        return context.Exception switch
        {
            ValidationException ex => (CreatedResult<BadRequestValidationObjectResult>(ex.Failures), (int)HttpStatusCode.BadRequest),
            NotFoundException => (CreatedResult<NotFoundObjectResult>(errorMessage, HttpStatusCode.NotFound), (int)HttpStatusCode.NotFound),
            UnauthorizedException => (CreatedResult<UnauthorizedObjectResult>(errorMessage, HttpStatusCode.Unauthorized), (int)HttpStatusCode.Unauthorized),
            BadRequestException => (CreatedResult<BadRequestObjectResult>(errorMessage, HttpStatusCode.BadRequest), (int)HttpStatusCode.BadRequest),
            RegisterException ex => (CreatedResult<BadRequestRegistrationObjectResult>(ex.Errors), (int)HttpStatusCode.BadRequest),
            _ => (new InternalServerErrorObjectResult(new ErrorDto(errorMessage,
                            nameof(HttpStatusCode.InternalServerError))), (int)HttpStatusCode.InternalServerError)
        };
    }

    private static IActionResult? CreatedResult<T>(IList<KeyValuePair<string, string>> errors)
        where T : ObjectResult =>
            (T?)Activator.CreateInstance(typeof(T), ResultDto<Unit>.CreateFromErrors(errors));

    private static IActionResult? CreatedResult<T>(string error, HttpStatusCode statusCode)
        where T : ObjectResult =>
            (T?)Activator.CreateInstance(typeof(T), ResultDto<Unit>.CreateFromErrors(error, statusCode));
}
