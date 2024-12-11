using PWMS.Application.Configurations.Inventory.Items.Items.Models;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Commands.Update;

public sealed record UpdateItemCommand(UpdateItemDto UpdateItemDto) : IRequest<Result<ItemDto>>;