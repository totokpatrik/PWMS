﻿namespace PWMS.Presentation.Rest.Filters.Results;

public sealed class BadRequestValidationObjectResult : BadRequestObjectResult
{
    public BadRequestValidationObjectResult(object? error) : base(error)
    {
    }
}
