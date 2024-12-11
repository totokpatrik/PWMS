using PWMS.Application.Configurations.Inventory.Items.Items.Models;
using PWMS.Application.Configurations.Inventory.Items.Items.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Items.Specifications;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Commands.Update;

public sealed class UpdateItemCommandHandler(IItemRepository itemRepository)
    : IRequestHandler<UpdateItemCommand, Result<ItemDto>>
{
    private readonly IItemRepository _itemRepository = itemRepository.ThrowIfNull();

    public async Task<Result<ItemDto>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _itemRepository
            .SingleOrDefaultAsync(new ItemByIdSpecification(request.UpdateItemDto.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Address), request.UpdateItemDto.Id);
        }

        entity.Update(request.UpdateItemDto.Name, request.UpdateItemDto.Description, request.UpdateItemDto.ShortDescription, request.UpdateItemDto.ReceiveStatus);

        await _itemRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        var dto = entity.Adapt<ItemDto>();

        return Result.Ok(dto);
    }
}
