using PWMS.Application.Auth.Repositories;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Core.Sites.Repositories;
using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Application.Core.Sites.Commands.Create;
public sealed class CreateSiteCommandHandler(
    ISiteRepository siteRepository,
    ICurrentUser currentUser,
    IAuthRepository authRepository) : IRequestHandler<CreateSiteCommand, Result<Guid>>
{
    private readonly ISiteRepository _siteRepository = siteRepository.ThrowIfNull();
    private readonly ICurrentUser _currentUser = currentUser.ThrowIfNull();
    private readonly IAuthRepository _authRepository = authRepository.ThrowIfNull();

    public async Task<Result<Guid>> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.Id;

        if (userId == null)
        {
            throw new NotFoundException(nameof(User));
        }

        var user = await _authRepository
            .GetByIdAsync(userId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User));
        }

        var entity = new Site(request.Name, user);
        user.SelectSite(entity);

        await _siteRepository
            .AddAsync(entity, cancellationToken);

        await _siteRepository
            .SaveChangesAsync(cancellationToken);

        return Result.Ok(entity.Id);
    }
}
