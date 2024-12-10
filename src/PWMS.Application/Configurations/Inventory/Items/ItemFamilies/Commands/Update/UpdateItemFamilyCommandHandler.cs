using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Models;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Repositories;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Specifications;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Repositories;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Specifications;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Commands.Update;
public sealed class UpdateItemFamilyCommandHandler(IItemFamilyRepository itemFamilyRepository, IItemFamilyGroupRepository itemFamilyGroupRepository)
    : IRequestHandler<UpdateItemFamilyCommand, Result<ItemFamilyDto>>
{
    private readonly IItemFamilyRepository _itemFamilyRepository = itemFamilyRepository.ThrowIfNull();
    private readonly IItemFamilyGroupRepository _itemFamilyGroupRepository = itemFamilyGroupRepository.ThrowIfNull();

    public async Task<Result<ItemFamilyDto>> Handle(UpdateItemFamilyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _itemFamilyRepository
            .SingleOrDefaultAsync(new ItemFamilyByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ItemFamily), request.Id);
        }

        var itemFamilyGroup = await _itemFamilyGroupRepository
            .SingleOrDefaultAsync(new ItemFamilyGroupByIdSpecification(request.ItemFamilyGroupId), cancellationToken)
            .ConfigureAwait(false);

        if (itemFamilyGroup == null)
        {
            throw new NotFoundException(nameof(ItemFamilyGroup), request.ItemFamilyGroupId);
        }

        entity.Update(request.Name, request.Description, itemFamilyGroup);

        await _itemFamilyRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        var dto = entity.Adapt<ItemFamilyDto>();

        return Result.Ok(dto);
    }
}
