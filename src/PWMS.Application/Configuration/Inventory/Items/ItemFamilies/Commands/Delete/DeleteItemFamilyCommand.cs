namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Commands.Delete;

public sealed record DeleteItemFamilyCommand(Guid Id) : IRequest<Result<Guid>>;