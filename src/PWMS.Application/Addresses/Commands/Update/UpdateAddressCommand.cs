using PWMS.Application.Addresses.Models;

namespace PWMS.Application.Addresses.Commands.Update;

public sealed record UpdateAddressCommand(Guid Id, string AddressLine) : IRequest<Result<AddressDto>>;