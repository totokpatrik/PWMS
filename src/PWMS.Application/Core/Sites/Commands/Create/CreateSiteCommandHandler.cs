using PWMS.Application.Auth.Repositories;
using PWMS.Application.Auth.Specifications;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Core.Sites.Repositories;
using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Application.Core.Sites.Commands.Create;
public sealed class CreateSiteCommandHandler(
    ISiteRepository siteRepository,
    IAuthRepository authRepository,
    ICurrentUserService currentUserService) : IRequestHandler<CreateSiteCommand, Result<Guid>>
{
    private readonly ISiteRepository _siteRepository = siteRepository.ThrowIfNull();
    private readonly ICurrentUserService _currentUserService = currentUserService.ThrowIfNull();
    private readonly IAuthRepository _authRepository = authRepository.ThrowIfNull();

    public async Task<Result<Guid>> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
    {
        // The user who creates it will be the owner
        var userId = _currentUserService.GetCurrentUser()?.Id;
        if (userId == null)
        {
            throw new NotFoundException(nameof(User));
        }

        var user = await _authRepository
            .SingleOrDefaultAsync(new UserByIdSpecification(userId, false), cancellationToken)
            .ConfigureAwait(false);

        if (user == null)
        {
            throw new NotFoundException(nameof(User));
        }

        var entity = new Site(request.Name, user);
        await _siteRepository
            .AddAsync(entity, cancellationToken);

        await _siteRepository
            .SaveChangesAsync(cancellationToken);

        return Result.Ok(entity.Id);
    }
}
