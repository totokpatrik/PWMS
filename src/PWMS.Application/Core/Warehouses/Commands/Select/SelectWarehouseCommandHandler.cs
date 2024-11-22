using PWMS.Application.Auth.Repositories;
using PWMS.Application.Auth.Specifications;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Core.Warehouses.Repositories;
using PWMS.Application.Core.Warehouses.Specifications;
using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Application.Core.Warehouses.Commands.Select;
public class SelectWarehouseCommandHandler(IWarehouseRepository warehouseRepository,
                                      IAuthRepository authRepository,
                                      ICurrentUserService currentUserService) : IRequestHandler<SelectWarehouseCommand, Result<Token>>
{
    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository.ThrowIfNull();
    private readonly IAuthRepository _authRepository = authRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result<Token>> Handle(SelectWarehouseCommand request, CancellationToken cancellationToken)
    {
        // Check if current user exists
        var user = await _authRepository
            .SingleOrDefaultAsync(new UserByIdSpecification(_currentUserService.GetCurrentUser().Id))
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(User));

        // Check if current warehouse exists
        var warehouse = await _warehouseRepository
            .SingleOrDefaultAsync(new WarehouseByIdSpecification(request.Id, user.Id), cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(Warehouse), request.Id);

        // Select the site
        user.SelectWarehouse(warehouse);

        // Get the new token
        var token = await _authRepository
            .BuildToken(user.UserName);

        await _authRepository.SaveChangesAsync();

        return Result.Ok(token);
    }
}
