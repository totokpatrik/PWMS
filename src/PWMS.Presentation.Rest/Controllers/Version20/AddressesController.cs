using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PWMS.Application.Addresses.Commands.Create;
using PWMS.Presentation.Rest.Models.Result;

namespace PWMS.Presentation.Rest.Controllers.Version20;

[ApiVersion(VersionController.Version20)]
[Route("api/v{version:apiVersion}/addresses")]
public class AddressesController : BaseController
{
    public AddressesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Create(
    [FromBody] CreateAddressCommand command,
    CancellationToken cancellationToken)
    => (await Mediator.Send(command, cancellationToken)).ToResultDto();
}
