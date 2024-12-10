using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Models;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Specifications;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Queries.Get;

internal sealed class GetFootprintQueryHandler
    : PagingDbQueryHandlerDb<GetFootprintQuery, Result<CollectionViewModel<FootprintDto>>, FootprintDto>
{
    private readonly IFootprintRepository _footprintRepository;

    public GetFootprintQueryHandler(
        IFootprintRepository footprintRepository,
        IApplicationDbContext applicationDbContext,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(applicationDbContext, mapper, currentUserService)
    {
        _footprintRepository = footprintRepository.ThrowIfNull();
    }

    public async override Task<Result<CollectionViewModel<FootprintDto>>> Handle(GetFootprintQuery request, CancellationToken cancellationToken)
    {
        var specification = FootprintSpecification.Create(request.PageContext);

        var entities = await _footprintRepository
            .GetAllFootprints(specification, cancellationToken, request.PageContext.Filter);

        var count = await _footprintRepository.CountAsync();

        var dtos = await entities
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<List<FootprintDto>>()
            .ConfigureAwait(false);

        return Result.Ok(new CollectionViewModel<FootprintDto>(
            dtos, count));
    }
}
