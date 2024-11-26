namespace PWMS.Application.Core.Sites.Commands.Delete;

public sealed record DeleteSiteCommand(Guid Id) : IRequest<Result<Guid>>;