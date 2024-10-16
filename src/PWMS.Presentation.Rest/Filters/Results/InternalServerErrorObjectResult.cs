using Microsoft.AspNetCore.Mvc;

namespace My.Nkz.NewApp.Presentation.Rest.Filters.Results;

public sealed class InternalServerErrorObjectResult : ObjectResult
{
    public InternalServerErrorObjectResult(object? value) : base(value)
    {
    }
}
