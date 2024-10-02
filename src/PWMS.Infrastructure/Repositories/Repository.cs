using Microsoft.EntityFrameworkCore;
using PWMS.Application.Abstractions.Paging;
using PWMS.Application.Abstractions.Repositories;
using PWMS.Domain.Abstractions.Entities;
using PWMS.Infrastructure.Data;

namespace PWMS.Infrastructure.Repositories;

internal class Repository<T> : IRepository<T> where T : Aggregate
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entitySet;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _entitySet = _context.Set<T>();
    }
    public void Delete(T entity)
    {
        _entitySet.Remove(entity);
    }

    public IQueryable<T> GetAll(bool noTracking = true)
    {
        var set = _entitySet;

        if (noTracking)
        {
            return set.AsNoTracking();
        }
        return set;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _entitySet.FindAsync(id);
    }

    public void Insert(T entity)
    {
        _entitySet.Add(entity);
    }

    public void Insert(List<T> entities)
    {
        _entitySet.AddRange(entities);
    }

    public void Remove(IEnumerable<T> entitiesToRemove)
    {
        _entitySet.RemoveRange(entitiesToRemove);
    }
}
