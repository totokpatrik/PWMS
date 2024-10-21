using PWMS.Application.Auth.Commands.Login;
using PWMS.Application.Auth.Commands.Register;
using PWMS.Domain.Auth.Entities;
using PWMS.Presentation.Rest.Models.Result;

namespace PWMS.Presentation.Rest.Controllers.Version10;

[ApiVersion(VersionController.Version10)]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : BaseController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Login.
    /// </summary>
    [HttpPost]
    [Route("Login")]
    [ProducesResponseType(typeof(ResultDto<Token>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Token>>> Login(
    [FromBody] LoginCommand command,
    CancellationToken cancellationToken)
    => (await Mediator.Send(command, cancellationToken)).ToResultDto();

    /// <summary>
    /// Register.
    /// </summary>
    [HttpPost]
    [Route("Register")]
    [ProducesResponseType(typeof(ResultDto<Token>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Token>>> Register(
    [FromBody] RegisterCommand command,
    CancellationToken cancellationToken)
    => (await Mediator.Send(command, cancellationToken)).ToResultDto();
}
