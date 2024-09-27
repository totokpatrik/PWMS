using MediatR;

namespace PWMS.Api.Common;

[ApiController]
[Produces("application/json")]
public class BaseController : ControllerBase
{
    protected BaseController(ISender sender) => Sender = sender;
    protected ISender Sender { get; }
}
