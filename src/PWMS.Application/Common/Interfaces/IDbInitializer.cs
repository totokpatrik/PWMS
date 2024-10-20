
namespace PWMS.Application.Common.Interfaces;

public interface IDbInitializer
{
    Task SeedAsync(IApplicationDbContext context, CancellationToken cancellationToken = default);
    Task SeedAsync(IApplicationDbContext applicationDbContext, IServiceScope scope, CancellationToken cancellationToken = default);
}
