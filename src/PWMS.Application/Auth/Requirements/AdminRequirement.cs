using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Common.Identity.Roles;
using System.Security.Claims;

namespace PWMS.Application.Auth.Requirements;

public class AdminRequirement : AuthorizationHandler<AdminRequirement>, IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        if (context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Any(c => c.Value == Role.Admin))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
