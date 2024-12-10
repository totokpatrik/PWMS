namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.Delete;

public sealed record DeleteFootprintCommand(Guid Id) : IRequest<Result<Guid>>;
