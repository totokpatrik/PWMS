using PWMS.Application.Abstractions.Paging;
using PWMS.Domain.Abstractions.Entities;

namespace PWMS.Application.Abstractions.Repositories;

public interface IRepository<T> where T : Aggregate
{
    IQueryable<T> GetAll(bool noTracking = true);
    Task<T?> GetByIdAsync(Guid id);
    void Insert(T entity);
    void Insert(List<T> entities);
    void Delete(T entity);
    void Remove(IEnumerable<T> entitiesToRemove);
}
