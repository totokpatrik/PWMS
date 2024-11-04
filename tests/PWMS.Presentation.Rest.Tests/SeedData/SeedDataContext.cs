using Microsoft.Extensions.DependencyInjection;
using PWMS.Application.Common.Interfaces;
using PWMS.Common.Extensions;
using PWMS.Domain.Addresses.Entities;
using PWMS.Persistence.PortgreSQL.Data;

namespace PWMS.Presentation.Rest.Tests.SeedData;

public sealed partial class SeedDataContext : DbInitializer
{
    public override async Task SeedAsync(IApplicationDbContext context, IServiceScope? scope, CancellationToken cancellationToken = default)
    {
        scope.ThrowIfNull();
        await context.AppDbContext.Set<Address>().AddRangeAsync(Addresses, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        await base.SeedAsync(context, scope, cancellationToken);
    }
}
