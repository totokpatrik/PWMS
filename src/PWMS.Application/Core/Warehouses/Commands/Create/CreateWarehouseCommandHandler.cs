using PWMS.Application.Auth.Repositories;
using PWMS.Application.Auth.Specifications;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Core.Sites.Repositories;
using PWMS.Application.Core.Sites.Specifications;
using PWMS.Application.Core.Warehouses.Repositories;
using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Sites.Entities;
using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Application.Core.Warehouses.Commands.Create;

public sealed class CreateWarehouseCommandHandler(
    ISiteRepository siteRepository,
    IWarehouseRepository warehouseRepository,
    IAuthRepository authRepository,
    ICurrentUserService currentUserService,
    ICurrentSiteService currentSiteService
    ) : IRequestHandler<CreateWarehouseCommand, Result<Guid>>
{
    private readonly ISiteRepository _siteRepository = siteRepository.ThrowIfNull();
    private readonly ICurrentUserService _currentUserService = currentUserService.ThrowIfNull();
    private readonly IAuthRepository _authRepository = authRepository.ThrowIfNull();
    private readonly ICurrentSiteService _currentSiteService = currentSiteService.ThrowIfNull();
    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository.ThrowIfNull();

    public async Task<Result<Guid>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var userId = (_currentUserService.GetCurrentUser()?.Id) ?? throw new NotFoundException(nameof(User));

        var user = await _authRepository
            .SingleOrDefaultAsync(new UserByIdSpecification(userId, false), cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(User));

        var siteId = (_currentSiteService.GetCurrentSite()?.Id) ?? throw new NotFoundException(nameof(Site));

        var site = await _siteRepository
            .SingleOrDefaultAsync(new SiteByIdSpecification(siteId, user.Id), cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(Site));


        var warehouse = new Warehouse(request.Name, site, user);
        await _warehouseRepository
            .AddAsync(warehouse, cancellationToken);

        await _warehouseRepository
            .SaveChangesAsync(cancellationToken);

        return Result.Ok(warehouse.Id);
    }
}
