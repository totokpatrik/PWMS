using PWMS.Application.Configurations.Inventory.Items.Footprints.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Specifications;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.Delete;
public class DeleteFootprintCommandHandler(IFootprintRepository footprintRepository)
    : IRequestHandler<DeleteFootprintCommand, Result<Guid>>
{
    private readonly IFootprintRepository _footprintRepository = footprintRepository.ThrowIfNull();
    public async Task<Result<Guid>> Handle(DeleteFootprintCommand request, CancellationToken cancellationToken)
    {
        var entity = await _footprintRepository
            .SingleOrDefaultAsync(new FootprintByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Footprint), request.Id);
        }

        await _footprintRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
        await _footprintRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entity.Id);
    }
}
