using Microsoft.AspNetCore.Identity;

namespace PWMS.Application.Auth.Commands.Login;

public sealed record LoginCommand(string UserName, string Password) : IRequest<Result<SignInResult>>
{
}
