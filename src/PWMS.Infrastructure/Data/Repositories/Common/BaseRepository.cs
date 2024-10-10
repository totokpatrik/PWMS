using Microsoft.EntityFrameworkCore;
using PWMS.Core.SharedKernel;
using PWMS.Infrastructure.Data.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PWMS.Infrastructure.Data.Repositories.Common;

/// <summary>
/// Base class for write-only repositories.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="Tkey">The type of the entity's key.</typeparam>
internal abstract class BaseRepository<TEntity, Tkey>(ApplicationDbContext context) : IBaseRepository<TEntity, Tkey>
    where TEntity : class, IEntity<Tkey>
    where Tkey : IEquatable<Tkey>
{
    private static readonly Func<ApplicationDbContext, Tkey, Task<TEntity>> GetByIdCompiledAsync =
        EF.CompileAsyncQuery((ApplicationDbContext context, Tkey id) =>
            context
                .Set<TEntity>()
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefault(entity => entity.Id.Equals(id)));

    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    protected readonly ApplicationDbContext Context = context;

    public void Add(TEntity entity) =>
        _dbSet.Add(entity);

    public void Update(TEntity entity) =>
        _dbSet.Update(entity);

    public void Remove(TEntity entity) =>
        _dbSet.Remove(entity);

    public async Task<TEntity> GetByIdAsync(Tkey id) =>
        await GetByIdCompiledAsync(Context, id);

    #region IDisposable

    // To detect redundant calls.
    private bool _disposed;

    ~BaseRepository() => Dispose(false);

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        // Dispose managed state (managed objects).
        if (disposing)
            Context.Dispose();

        _disposed = true;
    }

    #endregion
}