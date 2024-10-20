using Microsoft.AspNetCore.Identity;
using PWMS.Application.Common.Interfaces;
using PWMS.Domain.Auth.Entities;

namespace PWMS.Application.Auth.Commands.Login;

public sealed class LoginCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager, IApplicationDbContext dbContext) : IRequestHandler<LoginCommand, Result<SignInResult>>
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<SignInResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            throw new AuthorizationException("Username or password is incorrect.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            request.Password,
            lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new AuthorizationException("Username or password is incorrect.");
        }

        return Result.Ok(result);
    }
}
