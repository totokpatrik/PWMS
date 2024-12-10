using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Models;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Specifications;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Queries.GetById;
public sealed class GetFootprintByIdQueryHandler : HandlerDbQueryBase<GetFootprintByIdQuery, Result<FootprintDto>>
{
    private readonly IFootprintRepository _footprintRepository;

    public GetFootprintByIdQueryHandler(
        IFootprintRepository footprintRepository,
        IApplicationDbContext contextDb,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(contextDb, mapper, currentUserService)
    {
        _footprintRepository = footprintRepository.ThrowIfNull();
    }

    public async override Task<Result<FootprintDto>> Handle(GetFootprintByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _footprintRepository
            .SingleOrDefaultAsync(new FootprintByIdSpecification(request.Id, true), cancellationToken)
            .ConfigureAwait(false);

        entity.ThrowIfNull(new NotFoundException());

        var dtoEntity = await entity
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<FootprintDto>()
            .ConfigureAwait(false);

        return Result.Ok(dtoEntity);
    }
}
