using PWMS.Application.Core.Sites.Repositories;
using PWMS.Application.Core.Sites.Specifications;
using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Application.Core.Sites.Commands.Delete;

internal class DeleteSiteCommandHandler(ISiteRepository siteRepository) : IRequestHandler<DeleteSiteCommand, Result<Guid>>
{
    private readonly ISiteRepository _siteRepository = siteRepository.ThrowIfNull();

    public async Task<Result<Guid>> Handle(DeleteSiteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _siteRepository
            .SingleOrDefaultAsync(new SiteByIdSpecification(request.Id), cancellationToken)
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
