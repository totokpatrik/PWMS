using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PWMS.Application.Common.Interfaces;
using PWMS.Common.Extensions;
using PWMS.Domain.Addresses.Entities;
using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Common;
using PWMS.Persistence.PortgreSQL.Extensions;

namespace PWMS.Persistence.PortgreSQL.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>, IApplicationDbContext
{
    private ICurrentUserService _currentUserService = null!;
    private IDateTime _dateTime = null!;
    private IMediator _mediator = null!;
    private IDbInitializer _dbInitializer = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public void InitContext(ICurrentUserService currentUserService, IDbInitializer dbInitializer, IDateTime dateTime, IMediator mediator)
    {
        _dbInitializer = dbInitializer.ThrowIfNull();
        _currentUserService = currentUserService.ThrowIfNull();
        _dateTime = dateTime.ThrowIfNull();
        _mediator = mediator.ThrowIfNull();
    }

    public DbSet<Address> Addresses => Set<Address>();

    public DbContext AppDbContext => this;

    public IDbInitializer DbInitializer => _dbInitializer;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var currentUser = _currentUserService.CurrentUser;

        UpdateEntities(currentUser);

        var result = await base.SaveChangesAsync(cancellationToken);
        await DispatchDomainEventsAsync();

        return result;
    }

    public async Task MigrateAsync() => await AppDbContext.Database.MigrateAsync();

    public async Task SeedAsync() => await _dbInitializer.SeedAsync(this);

    private async Task DispatchDomainEventsAsync()
    {
        var domainEntities = ChangeTracker
            .Entries<IEntity>()
            .Where(x => x.Entity.DomainEvents.Count != 0);
        await _mediator.DispatchDomainEventsAsync(domainEntities);
    }

    private void UpdateEntities(ICurrentUser currentUser)
    {
        foreach (var entry in ChangeTracker.Entries<IBaseAuditableEntity<string>>())
        {
            if (entry.Entity.IsNew)
            {
                entry.State = EntityState.Added;
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = currentUser?.Id?.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    entry.Entity.Created = _dateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = currentUser?.Id?.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    entry.Entity.Modified = _dateTime.Now;
                    break;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.DataTimeConfigure();

        base.OnModelCreating(modelBuilder);
    }
}
