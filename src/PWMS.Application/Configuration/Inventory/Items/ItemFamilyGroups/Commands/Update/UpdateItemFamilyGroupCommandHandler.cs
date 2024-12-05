using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Models;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Repositories;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Specifications;
using PWMS.Domain.Inventories.Items.Entities;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Commands.Update;

public sealed class UpdateItemFamilyGroupCommandHandler(IItemFamilyGroupRepository itemFamilyGroupRepository)
    : IRequestHandler<UpdateItemFamilyGroupCommand, Result<ItemFamilyGroupDto>>
{
    private readonly IItemFamilyGroupRepository _itemFamilyGroupRepository = itemFamilyGroupRepository.ThrowIfNull();

    public async Task<Result<ItemFamilyGroupDto>> Handle(UpdateItemFamilyGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _itemFamilyGroupRepository
            .SingleOrDefaultAsync(new ItemFamilyGroupByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ItemFamilyGroup), request.Id);
        }

        entity.Update(request.Name, request.Description);

        await _itemFamilyGroupRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entity.Adapt<ItemFamilyGroupDto>());
    }
}
