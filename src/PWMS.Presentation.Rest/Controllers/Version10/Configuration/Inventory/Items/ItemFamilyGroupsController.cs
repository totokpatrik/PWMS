using FluentResults;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Commands.Create;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Commands.Delete;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Commands.DeleteRange;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Commands.Update;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Models;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Queries.Get;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Queries.GetById;
using PWMS.Presentation.Rest.Models.Result;
using System.ComponentModel.DataAnnotations;

namespace PWMS.Presentation.Rest.Controllers.Version10.Configuration.Inventory.Items;

[ApiVersion(VersionController.Version10)]
[Authorize]
[Route("api/v{version:apiVersion}/configuration/inventory/items/[controller]")]
public class ItemFamilyGroupsController : BaseController
{
    public ItemFamilyGroupsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    /// <summary>
    /// Creates item family group.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Create(
    [FromBody] CreateItemFamilyGroupDto createItemFamilyGroupDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new CreateItemFamilyGroupCommand(createItemFamilyGroupDto.Name, createItemFamilyGroupDto.Description), cancellationToken)).ToResultDto();

    /// <summary>
    /// Updates item family group.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(ResultDto<ItemFamilyGroupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<ItemFamilyGroupDto>>> Update(
    [FromBody][Required] UpdateItemFamilyGroupDto updateItemFamilyGroupDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new UpdateItemFamilyGroupCommand(
        updateItemFamilyGroupDto.Id,
        updateItemFamilyGroupDto.Name,
        updateItemFamilyGroupDto.Description), cancellationToken)).ToResultDto();

    /// <summary>
    /// Deletes item family group.
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Delete(
    [FromBody][Required] DeleteItemFamilyGroupDto deleteItemFamilyGroupDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new DeleteItemFamilyGroupCommand(deleteItemFamilyGroupDto.Id), cancellationToken)).ToResultDto();

    /// <summary>
    /// Deletes item family group range.
    /// </summary>
    [HttpDelete("range")]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<List<Guid>>>> DeleteRange(
    [FromBody][Required] List<DeleteItemFamilyGroupDto> deleteItemFamilyGroupDtos,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new DeleteRangeItemFamilyGroupCommand(deleteItemFamilyGroupDtos.Select(a => a.Id).ToList()), cancellationToken)).ToResultDto();

    /// <summary>
    /// Gets item family groups with pagination.
    /// </summary>
    [HttpPost]
    [Route("page")]
    [ProducesResponseType(typeof(ResultDto<CollectionViewModel<ItemFamilyGroupDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<CollectionViewModel<ItemFamilyGroupDto>>>> Page(
    [FromBody] PageContext pageContext,
    CancellationToken cancellationToken)
    {
        return (await Mediator.Send(GetItemFamilyGroupQuery.Create(pageContext), cancellationToken)).ToResultDto();
    }

    /// <summary>
    /// Gets item family group by id.
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResultDto<ItemFamilyGroupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<ItemFamilyGroupDto>>> Get(Guid id, CancellationToken cancellationToken)
    {
        var itemFamilyGroup = await Mediator.Send(new GetItemFamilyGroupByIdQuery(id), cancellationToken);
        var itemFamilyGroupDto = await itemFamilyGroup
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<Result<ItemFamilyGroupDto>>()
            .ConfigureAwait(false);

        return itemFamilyGroupDto.ToResultDto();
    }
}
