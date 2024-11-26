using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Sites.Models;

namespace PWMS.Application.Core.Sites.Queries.Get;

public sealed class GetSiteQuery : PagingQuery<Result<CollectionViewModel<SiteDto>>>
{
    public GetSiteQuery(IPageContext pageContext) : base(pageContext)
    {
    }

    public static GetSiteQuery Create(PageContext pageContext) => new(pageContext);
}