using PWMS.Application.Abstractions.Commands;

namespace PWMS.Application.Addresses.Commands.DeleteAddress;

public sealed record DeleteAddressCommand(Guid Id) : Command;
