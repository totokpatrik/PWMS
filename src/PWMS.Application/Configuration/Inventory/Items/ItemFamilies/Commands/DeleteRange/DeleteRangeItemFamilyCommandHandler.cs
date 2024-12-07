using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Repositories;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Specifications;
using PWMS.Domain.Addresses.Entities;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Commands.DeleteRange;

internal class DeleteRangeItemFamilyCommandHandler(IItemFamilyRepository itemFamilyRepository)
    : IRequestHandler<DeleteRangeItemFamilyCommand, Result<List<Guid>>>
{
    private readonly IItemFamilyRepository _itemFamilyRepository = itemFamilyRepository.ThrowIfNull();
    public async Task<Result<List<Guid>>> Handle(DeleteRangeItemFamilyCommand request, CancellationToken cancellationToken)
    {
        var entities = new List<ItemFamily>();
        foreach (var id in request.Ids)
        {
            var entity = await _itemFamilyRepository
                .SingleOrDefaultAsync(new ItemFamilyByIdSpecification(id), cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Address), id);
            }

            entities.Add(entity);
        }

        await _itemFamilyRepository.DeleteRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        await _itemFamilyRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entities.Select(e => e.Id).ToList());
    }
}
