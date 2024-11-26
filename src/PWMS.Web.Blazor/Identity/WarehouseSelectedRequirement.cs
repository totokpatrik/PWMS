using Microsoft.AspNetCore.Authorization;
using PWMS.Application.Common.Identity.Roles;
using System.Security.Claims;

namespace PWMS.Web.Blazor.Identity;

public class WarehouseSelectedRequirement : AuthorizationHandler<WarehouseSelectedRequirement>, IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WarehouseSelectedRequirement requirement)
    {
        if (context.User.Claims
            .Where(c => c.Type == ClaimTypes.Role).Any(c => c.Value == Role.WarehouseOwner || c.Value == Role.WarehouseAdmin || c.Value == Role.WarehouseUser))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
