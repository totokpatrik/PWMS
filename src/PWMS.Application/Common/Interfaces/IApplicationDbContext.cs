namespace PWMS.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<T> Set<T>()
        where T : class;

    DbContext AppDbContext { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task MigrateAsync();

    Task SeedAsync(IServiceScope scope);
}
