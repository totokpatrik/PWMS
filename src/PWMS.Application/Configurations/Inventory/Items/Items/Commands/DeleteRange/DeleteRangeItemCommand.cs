namespace PWMS.Application.Configurations.Inventory.Items.Items.Commands.DeleteRange;
public sealed record DeleteRangeItemCommand(List<Guid> Ids) : IRequest<Result<List<Guid>>>;