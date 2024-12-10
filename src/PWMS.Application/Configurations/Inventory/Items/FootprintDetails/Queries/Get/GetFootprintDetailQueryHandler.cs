using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Repositories;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Specifications;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Queries.Get;

internal sealed class GetFootprintDetailQueryHandler
    : PagingDbQueryHandlerDb<GetFootprintDetailQuery, Result<CollectionViewModel<FootprintDetailDto>>, FootprintDetailDto>
{
    private readonly IFootprintDetailRepository _footprintDetailRepository;

    public GetFootprintDetailQueryHandler(
        IFootprintDetailRepository footprintDetailRepository,
        IApplicationDbContext applicationDbContext,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(applicationDbContext, mapper, currentUserService)
    {
        _footprintDetailRepository = footprintDetailRepository.ThrowIfNull();
    }

    public async override Task<Result<CollectionViewModel<FootprintDetailDto>>> Handle(GetFootprintDetailQuery request, CancellationToken cancellationToken)
    {
        var specification = FootprintDetailSpecification.Create(request.PageContext);

        var entities = await _footprintDetailRepository
            .GetAllFootprints(specification, cancellationToken, request.PageContext.Filter);

        var count = await _footprintDetailRepository.CountAsync();

        var dtos = await entities
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<List<FootprintDetailDto>>()
            .ConfigureAwait(false);

        return Result.Ok(new CollectionViewModel<FootprintDetailDto>(
            dtos, count));
    }
}
