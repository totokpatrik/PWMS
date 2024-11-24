using Castle.DynamicLinqQueryBuilder;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Core.Warehouses.Repositories;
using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Persistence.PortgreSQL.Core.Warehouses.Repositories;

public class WarehouseRepository : RepositoryBase<Warehouse>, IWarehouseRepository
{
    private readonly IApplicationDbContext _dbContext;

    public WarehouseRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Warehouse>> GetAllWarehouses(ISpecification<Warehouse> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbContext.Set<Warehouse>().AsQueryable().BuildQuery(filter),
            specification: specification)
            .Include(w => w.UsersSelected);

        return await queryResult.ToListAsync(cancellationToken);
    }
}
