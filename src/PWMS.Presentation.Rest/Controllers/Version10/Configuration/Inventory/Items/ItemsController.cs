using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.Items.Commands.Create;
using PWMS.Application.Configurations.Inventory.Items.Items.Commands.Delete;
using PWMS.Application.Configurations.Inventory.Items.Items.Commands.DeleteRange;
using PWMS.Application.Configurations.Inventory.Items.Items.Commands.Update;
using PWMS.Application.Configurations.Inventory.Items.Items.Models;
using PWMS.Application.Configurations.Inventory.Items.Items.Queries.Get;
using PWMS.Application.Configurations.Inventory.Items.Items.Queries.GetById;
using PWMS.Presentation.Rest.Models.Result;
using System.ComponentModel.DataAnnotations;

namespace PWMS.Presentation.Rest.Controllers.Version10.Configuration.Inventory.Items;

[ApiVersion(VersionController.Version10)]
[Authorize]
[Route("api/v{version:apiVersion}/configuration/inventory/items/[controller]")]
public class ItemsController : BaseController
{
    public ItemsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }


    /// <summary>
    /// Creates item.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Create(
    [FromBody] CreateItemDto createItemDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new CreateItemCommand(createItemDto), cancellationToken)).ToResultDto();

    /// <summary>
    /// Updates item.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(ResultDto<ItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<ItemDto>>> Update(
    [FromBody][Required] UpdateItemDto updateItemDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new UpdateItemCommand(updateItemDto), cancellationToken)).ToResultDto();

    /// <summary>
    /// Deletes item.
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Delete(
    [FromBody][Required] DeleteItemDto deleteItemDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new DeleteItemCommand(deleteItemDto.Id), cancellationToken)).ToResultDto();

    /// <summary>
    /// Deletes item range.
    /// </summary>
    [HttpDelete("range")]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<List<Guid>>>> DeleteRange(
    [FromBody][Required] List<DeleteItemDto> deleteItemDtos,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new DeleteRangeItemCommand(deleteItemDtos.Select(a => a.Id).ToList()), cancellationToken)).ToResultDto();


    /// <summary>
    /// Gets items with pagination.
    /// </summary>
    [HttpPost]
    [Route("page")]
    [ProducesResponseType(typeof(ResultDto<CollectionViewModel<ItemDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<CollectionViewModel<ItemDto>>>> Page(
    [FromBody] PageContext pageContext,
    CancellationToken cancellationToken)
    => (await Mediator.Send(GetItemQuery.Create(pageContext), cancellationToken)).ToResultDto();

    /// <summary>
    /// Gets item by id.
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResultDto<ItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<ItemDto>>> Get(Guid id, CancellationToken cancellationToken)
    => (await Mediator.Send(new GetItemByIdQuery(id), cancellationToken)).ToResultDto();
}