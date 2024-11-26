using PWMS.Application.Auth.Repositories;
using PWMS.Application.Auth.Specifications;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Core.Sites.Repositories;
using PWMS.Application.Core.Sites.Specifications;
using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Application.Core.Sites.Commands.Select;

public class SelectSiteCommandHandler(ISiteRepository siteRepository,
                                      IAuthRepository authRepository,
                                      ICurrentUserService currentUserService) : IRequestHandler<SelectSiteCommand, Result<Token>>
{
    private readonly ISiteRepository _siteRepository = siteRepository.ThrowIfNull();
    private readonly IAuthRepository _authRepository = authRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result<Token>> Handle(SelectSiteCommand request, CancellationToken cancellationToken)
    {
        // Check if current user exists
        var user = await _authRepository
            .SingleOrDefaultAsync(new UserByIdSpecification(_currentUserService.GetCurrentUser().Id))
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(User));

        // Check if current site exists
        var entity = await _siteRepository
            .SingleOrDefaultAsync(new SiteByIdSpecification(request.Id, user.Id), cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(Site), request.Id);

        // Select the site
        user.SelectSite(entity);

        await _authRepository.SelectSite(entity, user.UserName!);

        // Get the new token
        var token = await _authRepository
            .BuildToken(user.UserName!);

        await _authRepository.SaveChangesAsync();

        return Result.Ok(token);
    }
}
