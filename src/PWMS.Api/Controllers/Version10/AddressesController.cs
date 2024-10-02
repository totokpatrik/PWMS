using Asp.Versioning;
using PWMS.Api.Infrastructure.ActionResults;
using PWMS.Application.Abstractions.Paging;
using PWMS.Application.Addresses.Commands.CreateAddress;
using PWMS.Application.Addresses.Commands.DeleteAddress;
using PWMS.Application.Addresses.Commands.UpdateAddress;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Addresses.Queries.GetAddress;
using PWMS.Application.Addresses.Queries.GetAddresses;

namespace PWMS.Api.Controllers.Version10;

[ApiVersion(VersionController.Version10)]
[Route("api/v{version:apiVersion}/addresses")]
public class AddressesController : BaseController
{
    protected AddressesController(ISender sender) : base(sender) { }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var forecast = await Sender.Send(new GetAddressQuery(id));
        return Ok(forecast);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<AddressDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] PaginationRequest paginationRequest)
    {
        var result = await Sender.Send(new GetAddressesQuery(paginationRequest));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] AddressCreateDto address)
    {
        var id = await Sender.Send(new CreateAddressCommand(
            address.Name,
            address.EmailAddress,
            address.AddressLine,
            address.Country,
            address.State,
            address.ZipCode));
        return CreatedAtAction(nameof(Get), new { id }, new CreatedResultEnvelope(id));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid id, [FromBody] AddressUpdateDto address)
    {
        await Sender.Send(new UpdateAddressCommand(
            id,
            address.Name,
            address.EmailAddress,
            address.AddressLine,
            address.Country,
            address.State,
            address.ZipCode));
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Sender.Send(new DeleteAddressCommand(id));
        return NoContent();
    }
}
