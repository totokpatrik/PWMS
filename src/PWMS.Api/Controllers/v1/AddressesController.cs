using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PWMS.Api.Extensions;
using PWMS.Api.Models;
using PWMS.Application.Addresses.Commands.CreateAddress;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PWMS.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class AddressesController(IMediator mediator) : ControllerBase
{
    ////////////////////////
    // POST: /api/addresses
    ////////////////////////

    /// <summary>
    /// Register a new address.
    /// </summary>
    /// <response code="200">Returns the Id of the new address.</response>
    /// <response code="400">Returns list of errors if the request is invalid.</response>
    /// <response code="500">When an unexpected internal error occurs on the server.</response>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<CreateAddressResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody][Required] CreateAddressCommand command) =>
        (await mediator.Send(command)).ToActionResult();
}
