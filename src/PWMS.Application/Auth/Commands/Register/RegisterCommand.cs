using PWMS.Domain.Auth.Entities;

namespace PWMS.Application.Auth.Commands.Register;

public sealed record RegisterCommand(string UserName, string Password) : IRequest<Result<Token>>
{
}
