using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Common.Identity.Roles;
using System.Security.Claims;

namespace PWMS.Web.Blazor.Identity;

public class SiteSelectedRequirement : AuthorizationHandler<SiteSelectedRequirement>, IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SiteSelectedRequirement requirement)
    {
        if (context.User.Claims
            .Where(c => c.Type == ClaimTypes.Role).Any(c => c.Value == Role.SiteOwner || c.Value == Role.SiteAdmin || c.Value == Role.SiteUser))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
