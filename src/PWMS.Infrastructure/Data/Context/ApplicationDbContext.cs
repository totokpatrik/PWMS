using Microsoft.EntityFrameworkCore;
using PWMS.Domain.Addresses.Aggregates;
using PWMS.Infrastructure.Data.Mappings;

namespace PWMS.Infrastructure.Data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbOptions)
    : BaseDbContext<ApplicationDbContext>(dbOptions)
{
    public DbSet<Address> Addresses => Set<Address>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AddressConfiguration());
    }
}
