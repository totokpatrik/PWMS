using Castle.DynamicLinqQueryBuilder;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Common.Interfaces;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Persistence.PortgreSQL.Addresses.Repositories;

public class AddressRepository : RepositoryBase<Address>, IAddressRepository
{
    private readonly IApplicationDbContext _dbContext;

    public AddressRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
        _dbContext = dbContext;
    }
    public AddressRepository(IApplicationDbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext.AppDbContext, specificationEvaluator)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Address>> GetAllAddresses(ISpecification<Address> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbContext.Set<Address>().AsQueryable().BuildQuery(filter),
            specification: specification);

        return await queryResult.ToListAsync(cancellationToken);
    }
}
