using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Warehouses.Models;
using PWMS.Application.Core.Warehouses.Repositories;
using PWMS.Application.Core.Warehouses.Specifications;

namespace PWMS.Application.Core.Warehouses.Queries.Get;

public sealed class GetWarehouseQueryHandler
    : PagingDbQueryHandlerDb<GetWarehouseQuery, Result<CollectionViewModel<WarehouseDto>>, WarehouseDto>
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetWarehouseQueryHandler(
        IWarehouseRepository warehouseRepository,
        IApplicationDbContext applicationDbContext,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(applicationDbContext, mapper, currentUserService)
    {
        _warehouseRepository = warehouseRepository.ThrowIfNull();
        _currentUserService = currentUserService.ThrowIfNull();
    }

    public async override Task<Result<CollectionViewModel<WarehouseDto>>> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
    {
        var specification = WarehouseSpecification.Create(request.PageContext, _currentUserService.GetCurrentUser().Id);

        var entities = await _warehouseRepository
            .GetAllWarehouses(specification, cancellationToken, request.PageContext.Filter);

        var warehouseCountSpecification = new WarehouseCountSpecification(_currentUserService.GetCurrentUser().Id);
        var count = await _warehouseRepository.CountAsync(warehouseCountSpecification);

        var dtoSites = await entities
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<List<WarehouseDto>>()
            .ConfigureAwait(false);

        return Result.Ok(new CollectionViewModel<WarehouseDto>(
            dtoSites, count));
    }
}
