using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Sites.Models;
using PWMS.Application.Core.Sites.Repositories;
using PWMS.Application.Core.Sites.Specifications;

namespace PWMS.Application.Core.Sites.Queries.Get;

internal sealed class GetSiteQueryHandler
    : PagingDbQueryHandlerDb<GetSiteQuery, Result<CollectionViewModel<SiteDto>>, SiteDto>
{
    private readonly ISiteRepository _siteRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetSiteQueryHandler(
        ISiteRepository siteRepository,
        IApplicationDbContext applicationDbContext,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(applicationDbContext, mapper, currentUserService)
    {
        _siteRepository = siteRepository.ThrowIfNull();
        _currentUserService = currentUserService;
    }

    public async override Task<Result<CollectionViewModel<SiteDto>>> Handle(GetSiteQuery request, CancellationToken cancellationToken)
    {
        var specification = SiteSpecification.Create(request.PageContext, _currentUserService.GetCurrentUser().Id);

        var entities = await _siteRepository
            .GetAllSites(specification, cancellationToken, request.PageContext.Filter);

        var count = await _siteRepository.CountAsync();

        var dtoSites = await entities
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<List<SiteDto>>()
            .ConfigureAwait(false);

        return Result.Ok(new CollectionViewModel<SiteDto>(
            dtoSites, count));
    }
}
