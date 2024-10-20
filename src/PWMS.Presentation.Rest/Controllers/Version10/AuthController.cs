using PWMS.Application.Addresses.Commands.Create;
using PWMS.Application.Auth.Commands.Login;
using PWMS.Presentation.Rest.Models.Result;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

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
    [ProducesResponseType(typeof(ResultDto<SignInResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<SignInResult>>> Login(
    [FromBody] LoginCommand command,
    CancellationToken cancellationToken)
    => (await Mediator.Send(command, cancellationToken)).ToResultDto();
}
