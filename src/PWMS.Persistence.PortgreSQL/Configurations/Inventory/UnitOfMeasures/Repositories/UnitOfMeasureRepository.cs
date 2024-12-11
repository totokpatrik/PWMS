using PWMS.Application.Common.Interfaces;
using PWMS.Application.Configurations.Inventory.UnitOfMeasures.Repositories;
using PWMS.Domain.Inventories.Entities;

namespace PWMS.Persistence.PortgreSQL.Configurations.Inventory.UnitOfMeasures.Repositories;

public class UnitOfMeasureRepository : RepositoryBase<UnitOfMeasure>, IUnitOfMeasureRepository
{
    private readonly IApplicationDbContext _dbContext;

    public UnitOfMeasureRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
        _dbContext = dbContext;
    }
}
