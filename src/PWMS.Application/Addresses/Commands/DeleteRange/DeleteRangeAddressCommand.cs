namespace PWMS.Application.Addresses.Commands.DeleteRange;

public sealed record DeleteRangeAddressCommand(List<Guid> Ids) : IRequest<Result<List<Guid>>>;