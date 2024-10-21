namespace PWMS.Application.Tests.SeedData;

using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PWMS.Domain.Addresses.Entities;

public sealed partial class SeedDataContext : IDbInitializer
{
    public async Task SeedAsync(IApplicationDbContext context, CancellationToken cancellationToken)
    {
        await context.AppDbContext.Set<Address>().AddRangeAsync(Addresses, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task SeedAsync(IApplicationDbContext context, IServiceScope scope, CancellationToken cancellationToken = default)
    {
        await context.AppDbContext.Set<Address>().AddRangeAsync(Addresses, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
