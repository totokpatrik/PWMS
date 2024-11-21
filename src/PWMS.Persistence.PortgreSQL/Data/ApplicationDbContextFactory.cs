using PWMS.Application.Common.Interfaces;
using PWMS.Common.Extensions;

namespace PWMS.Persistence.PortgreSQL.Data;

public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
{
    private readonly IDbContextFactory<ApplicationDbContext> _pooledFactory;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDbInitializer _dbInitializer;
    private readonly IMediator _mediator;
    private readonly IDateTime _dateTime;
    private readonly ICurrentWarehouseService _currentWarehouseService;

    public ApplicationDbContextFactory(
            IDbContextFactory<ApplicationDbContext> pooledFactory,
            ICurrentUserService currentUserService,
            ICurrentWarehouseService currentWarehouseService,
            IDbInitializer dbInitializer,
            IMediator mediator,
            IDateTime dateTime)
    {
        _pooledFactory = pooledFactory.ThrowIfNull();
        _currentUserService = currentUserService.ThrowIfNull();
        _dbInitializer = dbInitializer.ThrowIfNull();
        _mediator = mediator.ThrowIfNull();
        _dateTime = dateTime.ThrowIfNull();
        _currentWarehouseService = currentWarehouseService;
    }

    public ApplicationDbContext CreateDbContext()
    {
        var context = _pooledFactory.CreateDbContext();
        context.InitContext(_currentUserService, _currentWarehouseService, _dbInitializer, _dateTime, _mediator);
        return context;
    }
}
