using PWMS.Application.Addresses.Models;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Commands.Update;

public sealed record UpdateAddressCommand(Guid Id, string AddressLine, AddressType addressType) : IRequest<Result<AddressDto>>;