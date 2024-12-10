using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Repositories;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Specifications;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Commands.Delete;

public class DeleteItemFamilyGroupCommandHandler(IItemFamilyGroupRepository itemFamilyGroupRepository)
    : IRequestHandler<DeleteItemFamilyGroupCommand, Result<Guid>>
{
    private readonly IItemFamilyGroupRepository _itemFamilyGroupRepository = itemFamilyGroupRepository.ThrowIfNull();

    public async Task<Result<Guid>> Handle(DeleteItemFamilyGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _itemFamilyGroupRepository
            .SingleOrDefaultAsync(new ItemFamilyGroupByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Address), request.Id);
        }

        await _itemFamilyGroupRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
        await _itemFamilyGroupRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entity.Id);
    }
}
