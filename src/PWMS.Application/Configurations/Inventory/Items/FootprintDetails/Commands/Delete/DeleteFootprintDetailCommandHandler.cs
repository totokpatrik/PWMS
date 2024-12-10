using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Repositories;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Specifications;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.Delete;
public class DeleteFootprintDetailCommandHandler(IFootprintDetailRepository footprintDetailRepository)
    : IRequestHandler<DeleteFootprintDetailCommand, Result<Guid>>
{
    private readonly IFootprintDetailRepository _footprintDetailRepository = footprintDetailRepository.ThrowIfNull();
    public async Task<Result<Guid>> Handle(DeleteFootprintDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _footprintDetailRepository
            .SingleOrDefaultAsync(new FootprintDetailByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(FootprintDetail), request.Id);
        }

        await _footprintDetailRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
        await _footprintDetailRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entity.Id);
    }
}
