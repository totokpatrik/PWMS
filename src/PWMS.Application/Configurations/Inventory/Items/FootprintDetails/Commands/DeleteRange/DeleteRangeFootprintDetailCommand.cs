namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.DeleteRange;

public sealed record DeleteRangeFootprintDetailCommand(List<Guid> Ids) : IRequest<Result<List<Guid>>>;