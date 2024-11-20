using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Auth.Requirements;

public class AdminRequirement : AuthorizationHandler<AdminRequirement>, IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {

        var asd = context.Resource;
        if (context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Where(c => c.Value == "Admin").Any())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
