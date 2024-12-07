namespace PWMS.Application.Configuration.Inventory.Items.Footprints.Commands.Create;

public sealed record CreateFootprintCommand(string Name, bool Default, Guid ItemId) : IRequest<Result<Guid>>;