using Microsoft.AspNetCore.Identity;
using PWMS.Application.Common.Interfaces;
using PWMS.Common.Extensions;
using PWMS.Domain.Auth.Entities;

namespace PWMS.Persistence.PortgreSQL.Data;

public class DbInitializer() : IDbInitializer
{
    public virtual async Task SeedAsync(
        IApplicationDbContext context,
        IServiceScope? scope = null,
        CancellationToken cancellationToken = default)
    {
        // Seeding the initial user with initial roles
        scope.ThrowIfNull();

        var userManager = scope!.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope!.ServiceProvider.GetRequiredService<RoleManager<Role>>();

        var initialRoles = InitialData.Roles;
        var initialUser = InitialData.User;

        var initialUser2 = InitialData.User2;

        foreach ( var role in initialRoles )
        {

            if (!await roleManager.RoleExistsAsync(role.Name!))
            {
                // Create the initial role
                await roleManager.CreateAsync(role);
            }
        }


        if (await userManager.FindByNameAsync(initialUser.UserName!) == null)
        {
            var user = InitialData.User;

            // Create password for the initial user
            var password = new PasswordHasher<User>();
            var hashed = password.HashPassword(user, "secret");
            user.PasswordHash = hashed;

            await userManager.CreateAsync(user);

            await userManager.AddToRoleAsync(user, InitialData.AdminRole.Name!);
        }

        if (await userManager.FindByNameAsync(initialUser2.UserName!) == null)
        {
            var user = InitialData.User2;

            // Create password for the initial user
            var password = new PasswordHasher<User>();
            var hashed = password.HashPassword(user, "secret");
            user.PasswordHash = hashed;

            await userManager.CreateAsync(user);

            await userManager.AddToRoleAsync(user, InitialData.AdminRole.Name!);
        }
    }
}
