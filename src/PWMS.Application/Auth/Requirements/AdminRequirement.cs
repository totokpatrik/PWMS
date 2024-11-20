using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Common.Identity.Roles;
using System.Security.Claims;

namespace PWMS.Application.Auth.Requirements;

public class AdminRequirement : AuthorizationHandler<AdminRequirement>, IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {

        var asd2 = context.User.Claims
            .Where(c => c.Type == ClaimTypes.Role);

        var asd = context.Resource;
        if (context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Where(c => c.Value == Role.Admin).Any())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
