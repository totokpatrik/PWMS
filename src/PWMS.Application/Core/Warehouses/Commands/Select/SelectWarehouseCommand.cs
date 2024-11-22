using PWMS.Domain.Auth.Entities;

namespace PWMS.Application.Core.Warehouses.Commands.Select;
public sealed record SelectWarehouseCommand(Guid Id) : IRequest<Result<Token>>;