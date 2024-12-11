using PWMS.Application.Configurations.Inventory.Items.Items.Repositories;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Commands.Create;

public sealed class CreateItemCommandHandler(IItemRepository itemRepository) : IRequestHandler<CreateItemCommand, Result<Guid>>
{
    private readonly IItemRepository _itemRepository = itemRepository.ThrowIfNull();
    public async Task<Result<Guid>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new Item(
            request.CreateItemDto.Name,
            request.CreateItemDto.Description,
            request.CreateItemDto.ShortDescription,
            request.CreateItemDto.ReceiveStatus);

        await _itemRepository
            .AddAsync(entity, cancellationToken);

        await _itemRepository
            .SaveChangesAsync(cancellationToken);

        return Result.Ok(entity.Id);
    }
}
