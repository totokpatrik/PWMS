using Castle.DynamicLinqQueryBuilder;
using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Application.Core.Warehouses.Repositories;

public interface IWarehouseRepository : IRepositoryBase<Warehouse>
{
    Task<List<Warehouse>> GetAllWarehouses(ISpecification<Warehouse> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter);
}