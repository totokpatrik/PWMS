using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Repositories;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Specifications;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Queries.GetById;

public sealed class GetFootprintDetailByIdQueryHandler : HandlerDbQueryBase<GetFootprintDetailByIdQuery, Result<FootprintDetailDto>>
{
    private readonly IFootprintDetailRepository _footprintDetailRepository;

    public GetFootprintDetailByIdQueryHandler(
        IFootprintDetailRepository footprintDetailRepository,
        IApplicationDbContext contextDb,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(contextDb, mapper, currentUserService)
    {
        _footprintDetailRepository = footprintDetailRepository.ThrowIfNull();
    }

    public async override Task<Result<FootprintDetailDto>> Handle(GetFootprintDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _footprintDetailRepository
            .SingleOrDefaultAsync(new FootprintDetailByIdSpecification(request.Id, true), cancellationToken)
            .ConfigureAwait(false);

        entity.ThrowIfNull(new NotFoundException());

        var dtoEntity = await entity
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<FootprintDetailDto>()
            .ConfigureAwait(false);

        return Result.Ok(dtoEntity);
    }
}
