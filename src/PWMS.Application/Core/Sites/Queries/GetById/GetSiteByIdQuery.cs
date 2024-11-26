using PWMS.Application.Core.Sites.Models;

namespace PWMS.Application.Core.Sites.Queries.GetById;

public sealed record GetSiteByIdQuery(Guid Id) : IRequest<Result<SiteDto>>;