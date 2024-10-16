using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PWMS.Application.Addresses.Commands.Create;
using PWMS.Presentation.Rest.Models.Result;

namespace PWMS.Presentation.Rest.Controllers.Version10;

[ApiVersion(VersionController.Version10)]
[Route("api/v{version:apiVersion}/addresses")]
public class AddressesController : BaseController
{
    public AddressesController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Creates address.
    /// </summary>
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
