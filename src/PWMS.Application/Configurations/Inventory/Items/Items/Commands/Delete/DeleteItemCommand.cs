namespace PWMS.Application.Configurations.Inventory.Items.Items.Commands.Delete;
public sealed record DeleteItemCommand(Guid Id) : IRequest<Result<Guid>>;