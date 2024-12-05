namespace PWMS.Application.Inventories.AlternateItems.Commands.Create;

public sealed record CreateAlternateItemCommand(string AddressLine) : IRequest<Result<Guid>>;