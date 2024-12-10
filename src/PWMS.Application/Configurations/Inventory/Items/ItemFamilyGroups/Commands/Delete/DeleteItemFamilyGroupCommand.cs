namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Commands.Delete;

public sealed record DeleteItemFamilyGroupCommand(Guid Id) : IRequest<Result<Guid>>;