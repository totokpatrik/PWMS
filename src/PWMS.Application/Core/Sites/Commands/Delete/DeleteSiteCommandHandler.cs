using PWMS.Application.Auth.Repositories;
using PWMS.Application.Auth.Specifications;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Core.Sites.Repositories;
using PWMS.Application.Core.Sites.Specifications;
using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Application.Core.Sites.Commands.Delete;

internal class DeleteSiteCommandHandler(ISiteRepository siteRepository,
                                        IAuthRepository authRepository,
                                        ICurrentUserService currentUserService) : IRequestHandler<DeleteSiteCommand, Result<Guid>>
{
    private readonly ISiteRepository _siteRepository = siteRepository.ThrowIfNull();
    private readonly IAuthRepository _authRepository = authRepository.ThrowIfNull();
    private readonly ICurrentUserService _currentUserService = currentUserService.ThrowIfNull();

    public async Task<Result<Guid>> Handle(DeleteSiteCommand request, CancellationToken cancellationToken)
    {
        // Check if current user exists
        var user = await _authRepository
            .SingleOrDefaultAsync(new UserByIdSpecification(_currentUserService.GetCurrentUser().Id))
            .ConfigureAwait(false);

        if (user == null)
        {
            throw new NotFoundException(nameof(User));
        }

        var entity = await _siteRepository
            .SingleOrDefaultAsync(new SiteByIdSpecification(request.Id, user.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Site), request.Id);
        }
        await _siteRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
        await _siteRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entity.Id);
    }
}
