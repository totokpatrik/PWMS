using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Repositories;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Commands.Create;

public sealed class CreateItemFamilyGroupCommandHandler(IItemFamilyGroupRepository itemFamilyGroupRepository)
    : IRequestHandler<CreateItemFamilyGroupCommand, Result<Guid>>
{
    private readonly IItemFamilyGroupRepository _itemFamilyGroupRepository = itemFamilyGroupRepository.ThrowIfNull();

    public async Task<Result<Guid>> Handle(CreateItemFamilyGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = new ItemFamilyGroup(request.Name, request.Description);

        await _itemFamilyGroupRepository
            .AddAsync(entity, cancellationToken);

        await _itemFamilyGroupRepository
            .SaveChangesAsync(cancellationToken);

        return Result.Ok(entity.Id);
    }
}
