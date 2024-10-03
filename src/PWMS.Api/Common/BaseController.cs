using MediatR;

namespace PWMS.Api.Common;

[ApiController]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    protected BaseController(IMediator mediator) => Mediator = mediator;

    protected IMediator Mediator { get; }
}
