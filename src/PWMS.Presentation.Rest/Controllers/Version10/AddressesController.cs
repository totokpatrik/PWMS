using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Addresses.Commands.Create;
using PWMS.Application.Addresses.Commands.Delete;
using PWMS.Application.Addresses.Commands.DeleteRange;
using PWMS.Application.Addresses.Commands.Update;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Addresses.Queries.Get;
using PWMS.Application.Addresses.Queries.GetById;
using PWMS.Application.Common.Paging;
using PWMS.Presentation.Rest.Models.Result;
using System.ComponentModel.DataAnnotations;

namespace PWMS.Presentation.Rest.Controllers.Version10;

[ApiVersion(VersionController.Version10)]
[Authorize]
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
    [FromBody] CreateAddressDto createAddressDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new CreateAddressCommand(createAddressDto.AddressLine), cancellationToken)).ToResultDto();

    /// <summary>
    /// Updates address.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(ResultDto<AddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<AddressDto>>> Update(
    [FromBody][Required] UpdateAddressDto updateAddressDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new UpdateAddressCommand(updateAddressDto.Id, updateAddressDto.AddressLine), cancellationToken)).ToResultDto();

    /// <summary>
    /// Deletes address.
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Delete(
    [FromBody][Required] DeleteAddressDto deleteAddressDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new DeleteAddressCommand(deleteAddressDto.Id), cancellationToken)).ToResultDto();

    /// <summary>
    /// Deletes address range.
    /// </summary>
    [HttpDelete("range")]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<List<Guid>>>> DeleteRange(
    [FromBody][Required] List<DeleteAddressDto> deleteAddressDtos,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new DeleteRangeAddressCommand(deleteAddressDtos.Select(a => a.Id).ToList()), cancellationToken)).ToResultDto();


    /// <summary>
    /// Gets addresses with pagination.
    /// </summary>
    /// <remarks>
    /// Sort list directions:
    /// 0 - None
    /// 1 - Asc
    /// 2 - Desc
    /// 
    /// Sample request:
    ///
    ///     {
    ///       "pageIndex": 1,
    ///       "pageSize": 10,
    ///       "filter": {"condition":"AND","rules":[{"id":"1","field":"AddressLine","type":"string","input":"select","operator":"equal","value":["string"]}]}
    ///     }
    ///     OR
    ///     {
    ///       "pageIndex": 1,
    ///       "pageSize": 10,
    ///       "filter": {"condition":"AND","rules":[{"id":"1","field":"AddressLine","type":"string","input":"select","operator":"equal","value":["string"]}]},
    ///       "listSort": [
    ///         {
    ///           "field": "AddressLine",
    ///           "direction": 1
    ///         }
    ///       ]
    ///     } 
    /// </remarks>
    [HttpPost]
    [Route("page")]
    [ProducesResponseType(typeof(ResultDto<CollectionViewModel<AddressDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<CollectionViewModel<AddressDto>>>> Page(
    [FromBody] PageContext pageContext,
    CancellationToken cancellationToken)
    => (await Mediator.Send(GetAddressQuery.Create(pageContext), cancellationToken)).ToResultDto();

    /// <summary>
    /// Gets address by id.
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResultDto<AddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<AddressDto>>> Get(Guid id, CancellationToken cancellationToken)
    => (await Mediator.Send(new GetAddressByIdQuery(id), cancellationToken)).ToResultDto();
}
