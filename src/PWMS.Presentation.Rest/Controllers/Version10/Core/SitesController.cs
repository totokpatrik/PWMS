﻿
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Sites.Commands.Create;
using PWMS.Application.Core.Sites.Commands.Select;
using PWMS.Application.Core.Sites.Models;
using PWMS.Application.Core.Sites.Queries.Get;
using PWMS.Application.Core.Sites.Queries.GetById;
using PWMS.Domain.Auth.Entities;
using PWMS.Presentation.Rest.Models.Result;

namespace PWMS.Presentation.Rest.Controllers.Version10.Core;

[ApiVersion(VersionController.Version10)]
[Authorize]
public class SitesController : BaseController
{
    public SitesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    /// <summary>
    /// Creates site.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Create(
    [FromBody] CreateSiteDto createSiteDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new CreateSiteCommand(createSiteDto.Name), cancellationToken)).ToResultDto();

    /// <summary>
    /// Selects site.
    /// </summary>
    [HttpPost]
    [Route("select")]
    [ProducesResponseType(typeof(ResultDto<Token>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Token>>> Select(
    [FromBody] SelectSiteDto selectSiteDto,
    CancellationToken cancellationToken)
    => (await Mediator.Send(new SelectSiteCommand(selectSiteDto.SiteId), cancellationToken)).ToResultDto();

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
    [ProducesResponseType(typeof(ResultDto<CollectionViewModel<SiteDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<CollectionViewModel<SiteDto>>>> Page(
    [FromBody] PageContext pageContext,
    CancellationToken cancellationToken)
    => (await Mediator.Send(GetSiteQuery.Create(pageContext), cancellationToken)).ToResultDto();

    /// <summary>
    /// Gets sites by id.
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResultDto<SiteDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<SiteDto>>> Get(Guid id, CancellationToken cancellationToken)
    => (await Mediator.Send(new GetSiteByIdQuery(id), cancellationToken)).ToResultDto();
}
