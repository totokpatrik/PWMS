using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Commands.Create;

public sealed record CreateAddressCommand(string AddressLine, AddressType AddressType) : IRequest<Result<Guid>>;