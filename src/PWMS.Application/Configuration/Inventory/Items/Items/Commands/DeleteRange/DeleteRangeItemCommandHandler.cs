using PWMS.Application.Configuration.Inventory.Items.Items.Repositories;
using PWMS.Application.Configuration.Inventory.Items.Items.Specifications;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configuration.Inventory.Items.Items.Commands.DeleteRange;

internal class DeleteRangeItemCommandHandler(IItemRepository itemRepository) : IRequestHandler<DeleteRangeItemCommand, Result<List<Guid>>>
{
    private readonly IItemRepository _itemRepository = itemRepository.ThrowIfNull();
    public async Task<Result<List<Guid>>> Handle(DeleteRangeItemCommand request, CancellationToken cancellationToken)
    {
        var entities = new List<Item>();
        foreach (var id in request.Ids)
        {
            var entity = await _itemRepository
                .SingleOrDefaultAsync(new ItemByIdSpecification(id), cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Item), id);
            }

            entities.Add(entity);
        }

        await _itemRepository.DeleteRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        await _itemRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entities.ConvertAll(e => e.Id));
    }
}
