using MediatR;

namespace PWMS.Application.Addresses.Commands.DeleteAddress;

public sealed record DeleteAddressCommand(Guid Id) : IRequest;
