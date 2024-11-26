using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Sites.Models;

namespace PWMS.Application.Core.Sites.Queries.Get;

public sealed class GetSiteQueryValidator
    : PagingQueryValidator<GetSiteQuery, Result<CollectionViewModel<SiteDto>>>
{
}
