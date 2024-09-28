using PWMS.Application.Addresses.Queries.GetAddresses;
using PWMS.Application.Common.Paging;

namespace PWMS.Api.Controllers.Version10;

[ApiVersion(VersionController.Version10)]
[Route("api/v{version:apiVersion}/addresses")]
public class AddressesController : BaseController
{
    protected AddressesController(ISender sender) : base(sender) { }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationRequest paginationRequest)
    {
        var result = await Sender.Send(new GetAddressesQuery(paginationRequest));

        return Ok(result);
    }
}
