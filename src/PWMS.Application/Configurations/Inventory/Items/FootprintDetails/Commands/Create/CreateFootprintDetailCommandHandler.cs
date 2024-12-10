using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Specifications;
using PWMS.Application.Configurations.Inventory.UnitOfMeasures.Repositories;
using PWMS.Application.Configurations.Inventory.UnitOfMeasures.Specifications;
using PWMS.Domain.Configuration.Inventory.Items.Entities;
using PWMS.Domain.Inventories.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.Create;

public sealed class CreateFootprintDetailCommandHandler(IFootprintDetailRepository footprintDetailRepository,
                                                        IUnitOfMeasureRepository unitOfMeasureRepository,
                                                        IFootprintRepository footprintRepository)
    : IRequestHandler<CreateFootprintDetailCommand, Result<Guid>>
{
    private readonly IFootprintDetailRepository _footprintDetailRepository = footprintDetailRepository.ThrowIfNull();
    private readonly IUnitOfMeasureRepository _unitOfMeasureRepository = unitOfMeasureRepository.ThrowIfNull();
    private readonly IFootprintRepository _footprintRepository = footprintRepository.ThrowIfNull();

    public async Task<Result<Guid>> Handle(CreateFootprintDetailCommand request, CancellationToken cancellationToken)
    {
        var unitOfMeasure = await
            _unitOfMeasureRepository.SingleOrDefaultAsync(
                new UnitOfMeasureByIdSpecification(request.CreateFootprintDetailDto.UnitOfMeasureId),
                cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(UnitOfMeasure));


        var footprint = await
            _footprintRepository.SingleOrDefaultAsync(
                new FootprintByIdSpecification(request.CreateFootprintDetailDto.FootprintId),
                cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(Footprint));

        var entity = new FootprintDetail(unitOfMeasure: unitOfMeasure,
                                         unitQuantity: request.CreateFootprintDetailDto.UnitQuantity,
                                         grossWeight: request.CreateFootprintDetailDto.GrossWeight,
                                         netWeight: request.CreateFootprintDetailDto.NetWeight,
                                         length: request.CreateFootprintDetailDto.Length,
                                         width: request.CreateFootprintDetailDto.Width,
                                         height: request.CreateFootprintDetailDto.Height,
                                         footprint: footprint);

        await _footprintDetailRepository
            .AddAsync(entity, cancellationToken);

        await _footprintDetailRepository
            .SaveChangesAsync(cancellationToken);

        return Result.Ok(entity.Id);
    }
}
