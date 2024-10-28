using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PWMS.Application.Common.Interfaces;
using PWMS.Domain.Addresses.Entities;
using PWMS.Domain.Auth.Entities;
using PWMS.Persistence.PortgreSQL.Data;

namespace PWMS.Presentation.Rest.Tests.SeedData;

internal sealed partial class SeedDataContext : IDbInitializer
{
    public async Task SeedAsync(IApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        await context.AppDbContext.Set<Address>().AddRangeAsync(Addresses, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task SeedAsync(IApplicationDbContext context, IServiceScope scope, CancellationToken cancellationToken = default)
    {
        await context.AppDbContext.Set<Address>().AddRangeAsync(Addresses, cancellationToken);

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        //Create initial user
        var user = InitialData.User;
        //Create password for the initial user
        var password = new PasswordHasher<User>();
        var hashed = password.HashPassword(user, "secret");
        user.PasswordHash = hashed;

        await userManager.CreateAsync(user);

        await context.SaveChangesAsync(cancellationToken);
    }
}
