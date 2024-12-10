using PWMS.Application.Configurations.Inventory.Items.Footprints.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Specifications;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.DeleteRange;
internal class DeleteRangeFootprintCommandHandler(IFootprintRepository footprintRepository)
    : IRequestHandler<DeleteRangeFootprintCommand, Result<List<Guid>>>
{
    private readonly IFootprintRepository _footprintRepository = footprintRepository.ThrowIfNull();
    public async Task<Result<List<Guid>>> Handle(DeleteRangeFootprintCommand request, CancellationToken cancellationToken)
    {
        var entities = new List<Footprint>();
        foreach (var id in request.Ids)
        {
            var entity = await _footprintRepository
                .SingleOrDefaultAsync(new FootprintByIdSpecification(id), cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Footprint), id);
            }

            entities.Add(entity);
        }

        await _footprintRepository.DeleteRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        await _footprintRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entities.Select(e => e.Id).ToList());
    }
}
