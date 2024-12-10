namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.DeleteRange;
public sealed record DeleteRangeFootprintCommand(List<Guid> Ids) : IRequest<Result<List<Guid>>>;