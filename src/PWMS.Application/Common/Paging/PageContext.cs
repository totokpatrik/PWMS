namespace PWMS.Application.Common.Paging;

using Castle.DynamicLinqQueryBuilder;
using System.Numerics;

public sealed record PageContext<T> : IPageContext<T>,
    IIncrementOperators<PageContext<T>>, IDecrementOperators<PageContext<T>>
    where T : class, new()
{
    public PageContext(
        int pageIndex,
        int pageSize,
        QueryBuilderFilterRule? filter = null,
        IEnumerable<SortDescriptor>? listSort = null)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Filter = filter ?? new QueryBuilderFilterRule();
        ListSort = listSort ?? Enumerable.Empty<SortDescriptor>();
    }

    public int PageIndex { get; private set; }

    public int PageSize { get; }

    public QueryBuilderFilterRule Filter { get; }

    public IEnumerable<SortDescriptor> ListSort { get; }

    public bool IsValid() => PageIndex > 0 && PageSize > 0;

    public static PageContext<T> operator ++(PageContext<T> obj)
    {
        obj.PageIndex++;
        return obj;
    }

    public static PageContext<T> operator --(PageContext<T> obj)
    {
        obj.PageIndex--;
        return obj;
    }
}
