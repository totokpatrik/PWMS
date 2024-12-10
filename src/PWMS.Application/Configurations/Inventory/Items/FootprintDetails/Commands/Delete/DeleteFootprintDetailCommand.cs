namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.Delete;

public sealed record DeleteFootprintDetailCommand(Guid Id) : IRequest<Result<Guid>>;