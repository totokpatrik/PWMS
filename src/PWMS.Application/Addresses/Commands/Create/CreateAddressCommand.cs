namespace PWMS.Application.Addresses.Commands.Create;

public sealed record CreateAddressCommand(string AddressLine) : IRequest<Result<Guid>>;