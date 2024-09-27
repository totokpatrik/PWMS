using MediatR;

namespace PWMS.Api.Controllers.Version10;

[ApiVersion(VersionController.Version10)]
[Route("api/v{version:apiVersion}/identity")]
public class IdentityController : BaseController
{
    protected IdentityController(ISender sender) : base(sender) { }


}
