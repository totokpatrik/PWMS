using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Warehouses.Commands.Create;
using PWMS.Application.Core.Warehouses.Commands.Select;
using PWMS.Application.Core.Warehouses.Models;
using PWMS.Application.Core.Warehouses.Queries.Get;
using PWMS.Domain.Auth.Entities;
using PWMS.Presentation.Rest.Models.Result;

namespace PWMS.Presentation.Rest.Controllers.Version10.Core;

[ApiVersion(VersionController.Version10)]
[Authorize]
public class WarehousesController : BaseController
{
    public WarehousesController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Creates warehouse.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Create(
    [FromBody] CreateWarehouseDto createWarehouseDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new CreateWarehouseCommand(createWarehouseDto.Name), cancellationToken)).ToResultDto();

    [HttpPost]
    [Route("select")]
    [ProducesResponseType(typeof(ResultDto<Token>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Token>>> Select(
    [FromBody] SelectWarehouseDto selectWarehouseDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new SelectWarehouseCommand(selectWarehouseDto.WarehouseId), cancellationToken)).ToResultDto();

    /// <summary>
    /// Gets warehouses with pagination.
    /// </summary>
    [HttpPost]
    [Route("page")]
    [ProducesResponseType(typeof(ResultDto<CollectionViewModel<WarehouseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<CollectionViewModel<WarehouseDto>>>> Page(
    [FromBody] PageContext pageContext,
    CancellationToken cancellationToken)
    => (await Mediator.Send(GetWarehouseQuery.Create(pageContext), cancellationToken)).ToResultDto();
}
