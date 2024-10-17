using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PWMS.Presentation.Rest.Filters.Results;

public sealed class BadRequestValidationObjectResult : BadRequestObjectResult
{
    public BadRequestValidationObjectResult(object? error) : base(error)
    {
    }

    public BadRequestValidationObjectResult(ModelStateDictionary modelState) : base(modelState)
    {
    }
}
