using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Models;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Repositories;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Specifications;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Queries.GetById;

public sealed class GetItemFamilyByIdQueryHandler : HandlerDbQueryBase<GetItemFamilyByIdQuery, Result<ItemFamilyDto>>
{
    private readonly IItemFamilyRepository _itemFamilyRepository;

    public GetItemFamilyByIdQueryHandler(
        IItemFamilyRepository itemFamilyRepository,
        IApplicationDbContext contextDb,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(contextDb, mapper, currentUserService)
    {
        _itemFamilyRepository = itemFamilyRepository.ThrowIfNull();
    }

    public async override Task<Result<ItemFamilyDto>> Handle(GetItemFamilyByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _itemFamilyRepository
            .SingleOrDefaultAsync(new ItemFamilyByIdSpecification(request.Id, true), cancellationToken)
            .ConfigureAwait(false);

        entity.ThrowIfNull(new NotFoundException());

        var dtoEntity = await entity
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<ItemFamilyDto>()
            .ConfigureAwait(false);

        return Result.Ok(dtoEntity);
    }
}
