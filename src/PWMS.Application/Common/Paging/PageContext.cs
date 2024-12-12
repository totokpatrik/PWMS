namespace PWMS.Application.Common.Paging;

using Castle.DynamicLinqQueryBuilder;

public sealed record PageContext : IPageContext
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

    public QueryBuilderFilterRule Filter { get; set; }

    public IEnumerable<SortDescriptor> ListSort { get; }
}
