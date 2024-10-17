namespace PWMS.Application.Addresses.Commands.Delete;

public sealed record DeleteAddressCommand(Guid Id) : IRequest<Result<Guid>>;
