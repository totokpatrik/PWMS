using MediatR;
using Microsoft.AspNetCore.Mvc;
using PWMS.Common.Extensions;

namespace PWMS.Presentation.Rest.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    protected BaseController(IMediator mediator) => Mediator = mediator.ThrowIfNull();

    protected IMediator Mediator { get; }
}
