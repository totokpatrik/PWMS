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

    public Task SeedAsync(IApplicationDbContext applicationDbContext, IServiceScope scope, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
