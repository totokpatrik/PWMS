using Microsoft.Extensions.DependencyInjection;
using PWMS.Application.Common.Interfaces;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Presentation.Rest.Tests.SeedData;

internal sealed partial class SeedDataContext : IDbInitializer
{
    public async Task SeedAsync(IApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        await context.AppDbContext.Set<Address>().AddRangeAsync(Addresses, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task SeedAsync(IApplicationDbContext applicationDbContext, IServiceScope scope, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
