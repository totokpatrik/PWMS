using PWMS.Application.Common.Interfaces;

namespace PWMS.Persistence.PortgreSQL.Data;

public class DbInitializer : IDbInitializer
{
    public Task SeedAsync(IApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
