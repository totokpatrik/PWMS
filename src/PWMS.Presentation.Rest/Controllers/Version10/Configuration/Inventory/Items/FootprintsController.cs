using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.Create;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.Delete;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.DeleteRange;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.Update;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Models;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Queries.Get;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Queries.GetById;
using PWMS.Presentation.Rest.Models.Result;
using System.ComponentModel.DataAnnotations;

namespace PWMS.Presentation.Rest.Controllers.Version10.Configuration.Inventory.Items;

[ApiVersion(VersionController.Version10)]
[Authorize]
[Route("api/v{version:apiVersion}/configuration/inventory/items/[controller]")]
public class FootprintsController : BaseController
{
    public FootprintsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }


    /// <summary>
    /// Creates item footprint.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Create(
    [FromBody] CreateFootprintDto createFootprintDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new CreateFootprintCommand(createFootprintDto.Name, createFootprintDto.Default, createFootprintDto.ItemId), cancellationToken)).ToResultDto();

    /// <summary>
    /// Updates item footprint.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(ResultDto<FootprintDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<FootprintDto>>> Update(
    [FromBody][Required] UpdateFootprintDto updateFootprintDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new UpdateFootprintCommand(
        updateFootprintDto.Id,
        updateFootprintDto.Name,
        updateFootprintDto.Default), cancellationToken)).ToResultDto();

    /// <summary>
    /// Deletes item footprint.
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Delete(
    [FromBody][Required] DeleteFootprintDto deleteFootprintDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new DeleteFootprintCommand(deleteFootprintDto.Id), cancellationToken)).ToResultDto();

    /// <summary>
    /// Deletes item footprint range.
    /// </summary>
    [HttpDelete("range")]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<List<Guid>>>> DeleteRange(
    [FromBody][Required] List<DeleteFootprintDto> deleteFootprintDtos,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new DeleteRangeFootprintCommand(deleteFootprintDtos.Select(a => a.Id).ToList()), cancellationToken)).ToResultDto();


    /// <summary>
    /// Gets item footprints with pagination.
    /// </summary>
    [HttpPost]
    [Route("page")]
    [ProducesResponseType(typeof(ResultDto<CollectionViewModel<FootprintDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<CollectionViewModel<FootprintDto>>>> Page(
    [FromBody] PageContext pageContext,
    CancellationToken cancellationToken)
    => (await Mediator.Send(GetFootprintQuery.Create(pageContext), cancellationToken)).ToResultDto();

    /// <summary>
    /// Gets item footprint by id.
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResultDto<FootprintDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<FootprintDto>>> Get(Guid id, CancellationToken cancellationToken)
    => (await Mediator.Send(new GetFootprintByIdQuery(id), cancellationToken)).ToResultDto();
}
