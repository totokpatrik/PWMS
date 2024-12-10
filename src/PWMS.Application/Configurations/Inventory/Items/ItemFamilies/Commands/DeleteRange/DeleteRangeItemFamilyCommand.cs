namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Commands.DeleteRange;

public sealed record DeleteRangeItemFamilyCommand(List<Guid> Ids) : IRequest<Result<List<Guid>>>;