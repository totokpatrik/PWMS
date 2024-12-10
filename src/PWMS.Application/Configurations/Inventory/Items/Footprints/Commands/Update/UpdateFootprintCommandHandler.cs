using PWMS.Application.Configurations.Inventory.Items.Footprints.Models;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Specifications;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.Update;
public sealed class UpdateFootprintCommandHandler(IFootprintRepository footprintRepository)
    : IRequestHandler<UpdateFootprintCommand, Result<FootprintDto>>
{
    private readonly IFootprintRepository _footprintRepository = footprintRepository.ThrowIfNull();

    public async Task<Result<FootprintDto>> Handle(UpdateFootprintCommand request, CancellationToken cancellationToken)
    {
        var entity = await _footprintRepository
            .SingleOrDefaultAsync(new FootprintByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Address), request.Id);
        }

        if (request.Default)
        {
            //Make every other footprint's default false
            await _footprintRepository.ResetDefaultToFalse();
        }

        entity.Update(request.Name, request.Default, request.ItemId);

        await _footprintRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        var dto = entity.Adapt<FootprintDto>();

        return Result.Ok(dto);
    }
}
