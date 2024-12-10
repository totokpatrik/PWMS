using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Repositories;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Specifications;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.Update;

public sealed class UpdateFootprintDetailCommandHandler(IFootprintDetailRepository footprintDetailRepository)
    : IRequestHandler<UpdateFootprintDetailCommand, Result<FootprintDetailDto>>
{
    private readonly IFootprintDetailRepository _footprintDetailRepository = footprintDetailRepository.ThrowIfNull();

    public async Task<Result<FootprintDetailDto>> Handle(UpdateFootprintDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _footprintDetailRepository
            .SingleOrDefaultAsync(new FootprintDetailByIdSpecification(request.Update.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Address), request.Update.Id);
        }

        entity.Update(unitQuantity: request.Update.UnitQuantity,
                      grossWeight: request.Update.GrossWeight,
                      netWeight: request.Update.NetWeight,
                      length: request.Update.Length,
                      width: request.Update.Width,
                      height: request.Update.Height);

        await _footprintDetailRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        var dto = entity.Adapt<FootprintDetailDto>();

        return Result.Ok(dto);
    }
}
