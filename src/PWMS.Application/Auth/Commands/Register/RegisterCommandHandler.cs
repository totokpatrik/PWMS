using PWMS.Application.Auth.Repositories;

using PWMS.Domain.Auth.Entities;

namespace PWMS.Application.Auth.Commands.Register;

public sealed class RegisterCommandHandler(IAuthRepository authRepository) : IRequestHandler<RegisterCommand, Result<Token>>
{
    private readonly IAuthRepository _authRepository = authRepository.ThrowIfNull();

    async Task<Result<Token>> IRequestHandler<RegisterCommand, Result<Token>>.Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = await _authRepository.Register(request.UserName, request.Password);

        return Result.Ok(result);
    }
}
