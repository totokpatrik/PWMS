using Microsoft.AspNetCore.Authorization;

namespace PWMS.Presentation.Rest.Controllers.Version10;

[ApiVersion(VersionController.Version10)]
[Authorize]
public class CoreController : BaseController
{
    public CoreController(IMediator mediator) : base(mediator)
    { }


}
