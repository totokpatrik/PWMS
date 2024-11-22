using PWMS.Domain.Auth.Entities;

namespace PWMS.Application.Core.Sites.Commands.Select;

public sealed record SelectSiteCommand(Guid Id) : IRequest<Result<Token>>;