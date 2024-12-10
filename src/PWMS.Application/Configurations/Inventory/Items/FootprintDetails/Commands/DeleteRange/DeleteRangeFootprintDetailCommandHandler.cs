using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Repositories;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Specifications;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.DeleteRange;

internal class DeleteRangeFootprintDetailCommandHandler(IFootprintDetailRepository footprintDetailRepository)
    : IRequestHandler<DeleteRangeFootprintDetailCommand, Result<List<Guid>>>
{
    private readonly IFootprintDetailRepository _footprintDetailRepository = footprintDetailRepository.ThrowIfNull();
    public async Task<Result<List<Guid>>> Handle(DeleteRangeFootprintDetailCommand request, CancellationToken cancellationToken)
    {
        var entities = new List<FootprintDetail>();
        foreach (var id in request.Ids)
        {
            var entity = await _footprintDetailRepository
                .SingleOrDefaultAsync(new FootprintDetailByIdSpecification(id), cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(FootprintDetail), id);
            }

            entities.Add(entity);
        }

        await _footprintDetailRepository.DeleteRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        await _footprintDetailRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entities.ConvertAll(e => e.Id));
    }
}
