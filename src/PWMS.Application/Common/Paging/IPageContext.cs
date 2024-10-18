using Castle.DynamicLinqQueryBuilder;

namespace PWMS.Application.Common.Paging;

public interface IPageContext<out T>
{
    int PageIndex { get; }

    int PageSize { get; }

    QueryBuilderFilterRule Filter { get; }

    IEnumerable<SortDescriptor> ListSort { get; }

    bool IsValid();
}
