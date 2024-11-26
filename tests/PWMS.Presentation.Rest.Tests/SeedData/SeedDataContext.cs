using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PWMS.Application.Common.Interfaces;
using PWMS.Common.Extensions;
using PWMS.Domain.Addresses.Entities;
using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Sites.Entities;
using PWMS.Domain.Core.Warehouses.Entities;
using PWMS.Persistence.PortgreSQL.Data;

namespace PWMS.Presentation.Rest.Tests.SeedData;

public sealed partial class SeedDataContext : DbInitializer
{
    public override async Task SeedAsync(IApplicationDbContext context, IServiceScope? scope, CancellationToken cancellationToken = default)
    {
        scope.ThrowIfNull();
        // Add initial users
        await AddInitialRoles(scope);
        await AddInitialUsersAndRoles(scope);

        await context.SaveChangesAsync(cancellationToken);

        await AddInitialSite(context);
        await AddInitialWarehouse(context);
        await AddInitialAddresses(context);

        await SelectSiteAndWarehouse(context);

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task SelectSiteAndWarehouse(IApplicationDbContext context)
    {
        var user = await context.AppDbContext.Set<User>().FirstOrDefaultAsync(u => u.UserName == AdminUser.UserName!);
        var site = await context.AppDbContext.Set<Site>().FirstOrDefaultAsync(s => s.Name == SiteName);
        var warehouse = await context.AppDbContext.Set<Warehouse>().FirstOrDefaultAsync(w => w.Name == WarehouseName);

        if (user != null && site != null && warehouse != null)
        {
            user.SelectSite(site);
            user.SelectWarehouse(warehouse);
        }
    }

    private async Task AddInitialWarehouse(IApplicationDbContext context)
    {
        var user = await context.AppDbContext.Set<User>().FirstOrDefaultAsync(u => u.UserName == AdminUser.UserName!);
        var site = await context.AppDbContext.Set<Site>().FirstOrDefaultAsync(s => s.Name == SiteName);
        if (user != null && site != null)
        {
            var warehouse = Warehouse(site, user);
            await context.AppDbContext.Set<Warehouse>().AddAsync(Warehouse(site, user));
        }
        await context.SaveChangesAsync();
    }

    private async Task AddInitialSite(IApplicationDbContext context)
    {
        var user = await context.AppDbContext.Set<User>().FirstOrDefaultAsync(u => u.UserName == AdminUser.UserName!);
        if (user != null)
        {
            await context.AppDbContext.Set<Site>().AddAsync(Site(user));
        }
        await context.SaveChangesAsync();
    }

    private async Task AddInitialAddresses(IApplicationDbContext context)
    {
        var warehouse = await context.AppDbContext.Set<Warehouse>().FirstOrDefaultAsync(w => w.Name == WarehouseName);
        if (warehouse != null)
        {
            foreach (var address in Addresses)
            {
                address.WarehouseId = warehouse.Id;
                await context.AppDbContext.Set<Address>().AddAsync(address);
            }
        }
        await context.SaveChangesAsync();
    }
    private async Task AddInitialRoles(IServiceScope? scope)
    {
        var roleManager = scope!.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        foreach (var role in Roles)
        {
            await roleManager.CreateAsync(role);
        }
    }

    private async Task AddInitialUsersAndRoles(IServiceScope? scope)
    {
        var userManager = scope!.ServiceProvider.GetRequiredService<UserManager<User>>();
        foreach (var user in Users)
        {
            // Create password for the initial user
            var password = new PasswordHasher<User>();
            var hashed = password.HashPassword(user, "secret");
            user.PasswordHash = hashed;

            await userManager.CreateAsync(user);

            await userManager.AddToRoleAsync(user, AdminRole.Name!);
        }
    }
}
