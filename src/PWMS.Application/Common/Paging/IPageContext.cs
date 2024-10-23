using Castle.DynamicLinqQueryBuilder;

namespace PWMS.Application.Common.Paging;

public interface IPageContext
{
    int PageIndex { get; }

    int PageSize { get; }

    QueryBuilderFilterRule Filter { get; }

    IEnumerable<SortDescriptor> ListSort { get; }

}
