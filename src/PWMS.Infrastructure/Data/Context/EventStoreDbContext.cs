using Microsoft.EntityFrameworkCore;
using PWMS.Core.SharedKernel;
using PWMS.Infrastructure.Data.Mappings;

namespace PWMS.Infrastructure.Data.Context;

public class EventStoreDbContext(DbContextOptions<EventStoreDbContext> dbOptions)
    : BaseDbContext<EventStoreDbContext>(dbOptions)
{
    public DbSet<EventStore> EventStores => Set<EventStore>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new EventStoreConfiguration());
    }
}