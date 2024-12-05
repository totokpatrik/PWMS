using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Repositories;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Specifications;
using PWMS.Domain.Inventories.Items.Entities;
namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Queries.GetById;

public sealed class GetItemFamilyGroupByIdQueryHandler : HandlerDbQueryBase<GetItemFamilyGroupByIdQuery, Result<ItemFamilyGroup>>
{
    private readonly IItemFamilyGroupRepository _itemFamilyGroupRepository;

    public GetItemFamilyGroupByIdQueryHandler(
        IItemFamilyGroupRepository itemFamilyGroupRepository,
        IApplicationDbContext contextDb,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(contextDb, mapper, currentUserService)
    {
        _itemFamilyGroupRepository = itemFamilyGroupRepository.ThrowIfNull();
    }

    public async override Task<Result<ItemFamilyGroup>> Handle(GetItemFamilyGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _itemFamilyGroupRepository
            .SingleOrDefaultAsync(new ItemFamilyGroupByIdSpecification(request.Id, true), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ItemFamilyGroup));
        }

        return Result.Ok(entity);
    }
}
