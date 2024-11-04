using PWMS.Application.Auth.Repositories;
using PWMS.Domain.Auth.Entities;

namespace PWMS.Application.Auth.Commands.Login;

public sealed class LoginCommandHandler(IAuthRepository authRepository) : IRequestHandler<LoginCommand, Result<Token>>
{
    private readonly IAuthRepository _authRepository = authRepository.ThrowIfNull();

    async Task<Result<Token>> IRequestHandler<LoginCommand, Result<Token>>.Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _authRepository.Login(request.UserName, request.Password);

        return Result.Ok(result);
    }
}
