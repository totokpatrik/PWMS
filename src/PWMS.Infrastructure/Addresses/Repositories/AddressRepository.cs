using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PWMS.Application.Addresses.Repositories;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Infrastructure.Addresses.Repositories;

public class AddressRepository : RepositoryBase<Address>, IAddressRepository
{
    private readonly DbContext _dbContext;
    private readonly DbSet<Address> _entitySet;
    public AddressRepository(DbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _entitySet = dbContext.Set<Address>();
    }

    public AddressRepository(DbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext, specificationEvaluator)
    {
        _dbContext = dbContext;
        _entitySet = dbContext.Set<Address>();
    }

    public IQueryable<Address> GetAll(bool noTracking = true)
    {
        var set = _entitySet;

        if (noTracking)
        {
            return set.AsNoTracking();
        }
        return set;
    }
}
