namespace PWMS.Application.Core.Warehouses.Commands.Create;

public sealed record CreateWarehouseCommand(string Name) : IRequest<Result<Guid>>;