namespace PWMS.Application.Core.Sites.Commands.Create;
public sealed record CreateSiteCommand(string Name) : IRequest<Result<Guid>>;
