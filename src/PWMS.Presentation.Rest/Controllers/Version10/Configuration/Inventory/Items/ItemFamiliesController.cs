using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Commands.Create;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Commands.Delete;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Commands.DeleteRange;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Commands.Update;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Models;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Queries.Get;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Queries.GetById;
using PWMS.Presentation.Rest.Models.Result;
using System.ComponentModel.DataAnnotations;

namespace PWMS.Presentation.Rest.Controllers.Version10.Configuration.Inventory.Items;

[ApiVersion(VersionController.Version10)]
[Authorize]
[Route("api/v{version:apiVersion}/configuration/inventory/items/[controller]")]
public class ItemFamiliesController : BaseController
{
    public ItemFamiliesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }


    /// <summary>
    /// Creates item family.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Create(
    [FromBody] CreateItemFamilyDto createItemFamilyDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new CreateItemFamilyCommand(createItemFamilyDto.Name, createItemFamilyDto.Description, createItemFamilyDto.ItemfamilyGroupId), cancellationToken)).ToResultDto();

    /// <summary>
    /// Updates item family.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(ResultDto<ItemFamilyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<ItemFamilyDto>>> Update(
    [FromBody][Required] UpdateItemFamilyDto updateItemFamilyDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new UpdateItemFamilyCommand(
        updateItemFamilyDto.Id,
        updateItemFamilyDto.Name,
        updateItemFamilyDto.Description,
        updateItemFamilyDto.ItemFamilyGroupId), cancellationToken)).ToResultDto();

    /// <summary>
    /// Deletes item family.
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Delete(
    [FromBody][Required] DeleteItemFamilyDto deleteItemFamilyDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new DeleteItemFamilyCommand(deleteItemFamilyDto.Id), cancellationToken)).ToResultDto();

    /// <summary>
    /// Deletes item family range.
    /// </summary>
    [HttpDelete("range")]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<List<Guid>>>> DeleteRange(
    [FromBody][Required] List<DeleteItemFamilyDto> deleteItemFamilyDtos,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new DeleteRangeItemFamilyCommand(deleteItemFamilyDtos.Select(a => a.Id).ToList()), cancellationToken)).ToResultDto();


    /// <summary>
    /// Gets item families with pagination.
    /// </summary>
    [HttpPost]
    [Route("page")]
    [ProducesResponseType(typeof(ResultDto<CollectionViewModel<ItemFamilyDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<CollectionViewModel<ItemFamilyDto>>>> Page(
    [FromBody] PageContext pageContext,
    CancellationToken cancellationToken)
    => (await Mediator.Send(GetItemFamilyQuery.Create(pageContext), cancellationToken)).ToResultDto();

    /// <summary>
    /// Gets item family by id.
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResultDto<ItemFamilyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<ItemFamilyDto>>> Get(Guid id, CancellationToken cancellationToken)
    => (await Mediator.Send(new GetItemFamilyByIdQuery(id), cancellationToken)).ToResultDto();
}
