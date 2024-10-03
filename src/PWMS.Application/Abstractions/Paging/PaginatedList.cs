using Microsoft.EntityFrameworkCore;

namespace PWMS.Application.Abstractions.Paging;
public class PaginatedList<TEntity>
    where TEntity : class
{
    public int PageNumber { get; }
    public int TotalPages { get; }
    public long TotalCount { get; }
    public IReadOnlyCollection<TEntity> Data { get; }

    public PaginatedList(IReadOnlyCollection<TEntity> data, int pageNumber, int pageSize, long count)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Data = data;
    }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    public static async Task<PaginatedList<TEntity>> CreateAsync(IQueryable<TEntity> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var data = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<TEntity>(data, pageNumber, pageSize, count);
    }
}
