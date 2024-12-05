using MapsterMapper;
using PWMS.Common.Extensions;

namespace PWMS.Presentation.Rest.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    protected BaseController(IMediator mediator, IMapper mapper)
    {
        Mediator = mediator.ThrowIfNull();
        Mapper = mapper.ThrowIfNull();
    }

    protected IMediator Mediator { get; }
    protected IMapper Mapper { get; }
}
