using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Core.Sites.Models;
using PWMS.Application.Core.Sites.Repositories;
using PWMS.Application.Core.Sites.Specifications;

namespace PWMS.Application.Core.Sites.Queries.GetById;

public sealed class GetSiteByIdQueryHandler : HandlerDbQueryBase<GetSiteByIdQuery, Result<SiteDto>>
{
    private readonly ISiteRepository _siteRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetSiteByIdQueryHandler(
        ISiteRepository siteRepository,
        IApplicationDbContext contextDb,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(contextDb, mapper, currentUserService)
    {
        _siteRepository = siteRepository.ThrowIfNull();
        _currentUserService = currentUserService;
    }

    public async override Task<Result<SiteDto>> Handle(GetSiteByIdQuery request, CancellationToken cancellationToken)
    {
        var site = await _siteRepository
            .SingleOrDefaultAsync(new SiteByIdSpecification(request.Id, _currentUserService.GetCurrentUser()!.Id!), cancellationToken)
            .ConfigureAwait(false);

        site.ThrowIfNull(new NotFoundException());

        var dtoItem = await site
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<SiteDto>()
            .ConfigureAwait(false);

        return Result.Ok(dtoItem);
    }
}
