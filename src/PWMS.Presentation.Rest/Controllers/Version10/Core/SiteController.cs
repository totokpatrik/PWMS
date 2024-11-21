
using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Core.Sites.Commands.Create;
using PWMS.Application.Core.Sites.Models;
using PWMS.Presentation.Rest.Models.Result;

namespace PWMS.Presentation.Rest.Controllers.Version10.Core;

[ApiVersion(VersionController.Version10)]
[Authorize]
public class SiteController : BaseController
{
    public SiteController(IMediator mediator) : base(mediator)
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
}
