using PWMS.Application.Abstractions.Commands;

namespace PWMS.Application.Addresses.Commands.CreateAddress;

public sealed record CreateAddressCommand(
    string Name,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode)
    : CreateCommand;