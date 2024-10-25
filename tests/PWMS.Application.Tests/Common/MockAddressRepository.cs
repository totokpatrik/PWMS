using Ardalis.Specification;
using Castle.DynamicLinqQueryBuilder;
using PWMS.Application.Addresses.Repositories;
using PWMS.Domain.Addresses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Tests.Common;

public class MockAddressRepository : IAddressRepository
{
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

    public Task<Address> SingleOrDefaultAsync(ISingleResultSpecification<Address> specification, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new Address(Guid.Parse("AB7D57B6-0EB1-4E7C-9147-A84B254034C4"), "Test address"));
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
}
