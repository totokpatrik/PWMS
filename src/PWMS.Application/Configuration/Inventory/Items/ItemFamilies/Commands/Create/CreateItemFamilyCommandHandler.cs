using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Repositories;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Repositories;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Commands.Create;

public sealed class CreateItemFamilyCommandHandler(IItemFamilyRepository itemFamilyRepository,
                                                   IItemFamilyGroupRepository itemFamilyGroupRepository)
    : IRequestHandler<CreateItemFamilyCommand, Result<Guid>>
{
    private readonly IItemFamilyRepository _itemFamilyRepository = itemFamilyRepository.ThrowIfNull();
    private readonly IItemFamilyGroupRepository _itemFamilyGroupRepository = itemFamilyGroupRepository.ThrowIfNull();

    public async Task<Result<Guid>> Handle(CreateItemFamilyCommand request, CancellationToken cancellationToken)
    {
        var itemFamilyGroup = await _itemFamilyGroupRepository.GetByIdAsync(request.ItemFamilyGroupId, cancellationToken)
            .ConfigureAwait(false);

        if (itemFamilyGroup == null)
        {
            throw new NotFoundException(nameof(itemFamilyGroup));
        }

        var itemFamily = new ItemFamily(request.Name, request.Description, itemFamilyGroup);

        await _itemFamilyRepository
            .AddAsync(itemFamily, cancellationToken);

        await _itemFamilyRepository
            .SaveChangesAsync(cancellationToken);

        return Result.Ok(itemFamily.Id);
    }
}
