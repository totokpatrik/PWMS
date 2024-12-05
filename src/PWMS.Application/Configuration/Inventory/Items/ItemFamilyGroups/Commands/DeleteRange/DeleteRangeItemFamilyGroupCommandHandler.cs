using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Repositories;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Specifications;
using PWMS.Domain.Inventories.Items.Entities;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Commands.DeleteRange;

internal class DeleteRangeItemFamilyGroupCommandHandler(IItemFamilyGroupRepository itemFamilyGroupRepository)
    : IRequestHandler<DeleteRangeItemFamilyGroupCommand, Result<List<Guid>>>
{
    private readonly IItemFamilyGroupRepository _itemFamilyGroupRepository = itemFamilyGroupRepository.ThrowIfNull();
    public async Task<Result<List<Guid>>> Handle(DeleteRangeItemFamilyGroupCommand request, CancellationToken cancellationToken)
    {
        var entities = new List<ItemFamilyGroup>();
        foreach (var id in request.Ids)
        {
            var entity = await _itemFamilyGroupRepository
                .SingleOrDefaultAsync(new ItemFamilyGroupByIdSpecification(id), cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ItemFamilyGroup), id);
            }

            entities.Add(entity);
        }

        await _itemFamilyGroupRepository.DeleteRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        await _itemFamilyGroupRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entities.Select(e => e.Id).ToList());
    }
}
