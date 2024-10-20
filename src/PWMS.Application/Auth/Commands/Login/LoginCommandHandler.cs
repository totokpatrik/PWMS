using Microsoft.AspNetCore.Identity;
using PWMS.Application.Addresses.Commands.Create;
using PWMS.Domain.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Auth.Commands.Login;

public sealed class LoginCommandHandler(SignInManager<User> signInManager) : IRequestHandler<LoginCommand, Result<SignInResult>>
{
    private readonly SignInManager<User> _signInManager = signInManager;

    public async Task<Result<SignInResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(
            request.UserName,
            request.Password,
            isPersistent: false,
            lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new AuthorizationException("Username or password is incorrect.");
        }

        return Result.Ok(result);
    }
}
