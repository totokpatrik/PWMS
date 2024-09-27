using MediatR;

namespace PWMS.Api.Controllers.Version10;

[ApiVersion(VersionController.Version10)]
[Route("api/v{version:apiVersion}/addresses")]
public class AddressesController : BaseController
{
    protected AddressesController(ISender sender) : base(sender) { }


}
