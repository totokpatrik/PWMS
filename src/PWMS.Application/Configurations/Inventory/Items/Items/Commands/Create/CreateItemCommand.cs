using PWMS.Application.Configurations.Inventory.Items.Items.Models;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Commands.Create;

public sealed record CreateItemCommand(CreateItemDto CreateItemDto) : IRequest<Result<Guid>>;