using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.Create;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.Delete;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.DeleteRange;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.Update;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Queries.Get;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Queries.GetById;
using PWMS.Presentation.Rest.Models.Result;
using System.ComponentModel.DataAnnotations;

namespace PWMS.Presentation.Rest.Controllers.Version10.Configuration.Inventory.Items;

[ApiVersion(VersionController.Version10)]
[Authorize]
[Route("api/v{version:apiVersion}/configuration/inventory/items/[controller]")]
public class FootprintDetailsController : BaseController
{
    public FootprintDetailsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    /// <summary>
    /// Creates item footprint detail.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Create(
    [FromBody] CreateFootprintDetailDto createFootprintDetailDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new CreateFootprintDetailCommand(createFootprintDetailDto), cancellationToken)).ToResultDto();

    /// <summary>
    /// Updates item footprint detail.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(ResultDto<FootprintDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<FootprintDetailDto>>> Update(
    [FromBody][Required] UpdateFootprintDetailDto updateFootprintDetailDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new UpdateFootprintDetailCommand(updateFootprintDetailDto), cancellationToken)).ToResultDto();

    /// <summary>
    /// Deletes item footprint detail.
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Delete(
    [FromBody][Required] DeleteFootprintDetailDto deleteFootprintDetailDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new DeleteFootprintDetailCommand(deleteFootprintDetailDto.Id), cancellationToken)).ToResultDto();

    /// <summary>
    /// Deletes item footprint detail range.
    /// </summary>
    [HttpDelete("range")]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<List<Guid>>>> DeleteRange(
    [FromBody][Required] List<DeleteFootprintDetailDto> deleteFootprintDetailDtos,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new DeleteRangeFootprintDetailCommand(deleteFootprintDetailDtos.Select(a => a.Id).ToList()), cancellationToken)).ToResultDto();


    /// <summary>
    /// Gets item footprint details with pagination.
    /// </summary>
    [HttpPost]
    [Route("page")]
    [ProducesResponseType(typeof(ResultDto<CollectionViewModel<FootprintDetailDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<CollectionViewModel<FootprintDetailDto>>>> Page(
    [FromBody] PageContext pageContext,
    CancellationToken cancellationToken)
    => (await Mediator.Send(GetFootprintDetailQuery.Create(pageContext), cancellationToken)).ToResultDto();

    /// <summary>
    /// Gets item footprint detail by id.
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResultDto<FootprintDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<FootprintDetailDto>>> Get(Guid id, CancellationToken cancellationToken)
    => (await Mediator.Send(new GetFootprintDetailByIdQuery(id), cancellationToken)).ToResultDto();
}
