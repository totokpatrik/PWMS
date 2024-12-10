using PWMS.Application.Configurations.Inventory.Items.Items.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Items.Specifications;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Commands.Delete;

public class DeleteItemCommandHandler(IItemRepository itemRepository) : IRequestHandler<DeleteItemCommand, Result<Guid>>
{
    private readonly IItemRepository _itemRepository = itemRepository.ThrowIfNull();
    public async Task<Result<Guid>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _itemRepository
            .SingleOrDefaultAsync(new ItemByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Address), request.Id);
        }

        await _itemRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
        await _itemRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entity.Id);
    }
}
