namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Commands.Delete;

public sealed record DeleteItemFamilyCommand(Guid Id) : IRequest<Result<Guid>>;