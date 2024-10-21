using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PWMS.Presentation.Rest.Filters.Results;

public sealed class BadRequestRegistrationObjectResult : BadRequestObjectResult
{
    public BadRequestRegistrationObjectResult(object? error) : base(error)
    {
    }

    public BadRequestRegistrationObjectResult(ModelStateDictionary modelState) : base(modelState)
    {
    }
}
