using Ardalis.Specification;
using Castle.DynamicLinqQueryBuilder;
using Microsoft.EntityFrameworkCore;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Common.Interfaces;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Tests.Common;

public class MockAddressRepository(IApplicationDbContext dbContext, ISpecificationEvaluator specificationEvaluator) : IAddressRepository
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly ISpecificationEvaluator _specificationEvaluator = specificationEvaluator;

    public Task<Address> AddAsync(Address entity, CancellationToken cancellationToken = default)
    {
        Thread.Sleep(5000);

        return Task.FromResult(entity);
    }

    public Task<IEnumerable<Address>> AddRangeAsync(IEnumerable<Address> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(ISpecification<Address> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<Address> AsAsyncEnumerable(ISpecification<Address> specification)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(ISpecification<Address> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Address entity, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(IEnumerable<Address> entities, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(ISpecification<Address> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Address> FirstOrDefaultAsync(ISpecification<Address> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<Address, TResult> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Address>> GetAllAddresses(ISpecification<Address> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter)
    {
        throw new NotImplementedException();
    }

    public Task<Address> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        throw new NotImplementedException();
    }

    public Task<Address> GetBySpecAsync(ISpecification<Address> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TResult?> GetBySpecAsync<TResult>(ISpecification<Address, TResult> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Address>> ListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Address>> ListAsync(ISpecification<Address> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TResult>> ListAsync<TResult>(ISpecification<Address, TResult> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(0);
    }

    public async Task<Address> SingleOrDefaultAsync(ISingleResultSpecification<Address> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
    }

    public Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<Address, TResult> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Address entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(IEnumerable<Address> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    protected virtual IQueryable<Address> ApplySpecification(ISpecification<Address> specification, bool evaluateCriteriaOnly = false)
    {
        return _specificationEvaluator.GetQuery(_dbContext.Set<Address>().AsQueryable(), specification, evaluateCriteriaOnly);
    }
}
