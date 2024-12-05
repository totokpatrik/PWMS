using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Repositories;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Specifications;
using PWMS.Domain.Inventories.Items.Entities;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Commands.Delete;

public class DeleteItemFamilyCommandHandler(IItemFamilyRepository itemFamilyRepository) : IRequestHandler<DeleteItemFamilyCommand, Result<Guid>>
{
    private readonly IItemFamilyRepository _itemFamilyRepository = itemFamilyRepository.ThrowIfNull();
    public async Task<Result<Guid>> Handle(DeleteItemFamilyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _itemFamilyRepository
            .SingleOrDefaultAsync(new ItemFamilyByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ItemFamily), request.Id);
        }

        await _itemFamilyRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
        await _itemFamilyRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entity.Id);
    }
}
