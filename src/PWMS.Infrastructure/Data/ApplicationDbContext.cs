using Microsoft.EntityFrameworkCore;
using PWMS.Domain.Addresses.Entities;
using System.Reflection;

namespace PWMS.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }
    public DbSet<Address> Addresses => Set<Address>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
