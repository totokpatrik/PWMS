using PWMS.Application.Configurations.Inventory.Items.Footprints.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Items.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Items.Specifications;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.Create;

public sealed class CreateFootprintCommandHandler(IFootprintRepository footprintRepository,
                                                  IItemRepository itemRepository)
    : IRequestHandler<CreateFootprintCommand, Result<Guid>>
{
    private readonly IFootprintRepository _footprintRepository = footprintRepository.ThrowIfNull();
    private readonly IItemRepository _itemRepository = itemRepository.ThrowIfNull();
    public async Task<Result<Guid>> Handle(CreateFootprintCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository
            .SingleOrDefaultAsync(new ItemByIdSpecification(request.ItemId), cancellationToken)
            .ConfigureAwait(false);

        if (item == null)
        {
            throw new NotFoundException(nameof(Item));
        }

        var entity = new Footprint(request.Name, request.Default, item);

        await _footprintRepository
            .AddAsync(entity, cancellationToken);

        await _footprintRepository
            .SaveChangesAsync(cancellationToken);

        return Result.Ok(entity.Id);
    }
}
