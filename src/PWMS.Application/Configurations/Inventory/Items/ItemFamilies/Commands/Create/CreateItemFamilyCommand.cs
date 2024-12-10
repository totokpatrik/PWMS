namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Commands.Create;

public sealed record CreateItemFamilyCommand(string Name, string? Description, Guid ItemFamilyGroupId) : IRequest<Result<Guid>>;