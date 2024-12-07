﻿using Microsoft.AspNetCore.Mvc;

namespace PWMS.Presentation.Rest.Filters.Results;

public sealed class InternalServerErrorObjectResult : ObjectResult
{
    public InternalServerErrorObjectResult(object? value) : base(value)
    {
    }
}
