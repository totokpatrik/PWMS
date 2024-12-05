namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Commands.DeleteRange;
public sealed record DeleteRangeItemFamilyGroupCommand(List<Guid> Ids) : IRequest<Result<List<Guid>>>;
