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
        // TODO seeding initial user
        if (!await context.Set<User>().AnyAsync(cancellationToken))
        {
            scope.ThrowIfNull();
            var userManager = scope!.ServiceProvider.GetRequiredService<UserManager<User>>();
            //Create initial user
            var user = InitialData.User;

            //Create password for the initial user
            var password = new PasswordHasher<User>();
            var hashed = password.HashPassword(user, "secret");
            user.PasswordHash = hashed;

            await userManager.CreateAsync(user);
        }
    }
}
