using PWMS.Application.Addresses.Commands.Create;
using PWMS.Application.Addresses.Repositories;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Configuration.Inventory.Items.Footprints.Commands.Create;

public sealed class CreateAddressCommandHandler(IAddressRepository addressRepository) : IRequestHandler<CreateAddressCommand, Result<Guid>>
{
    private readonly IAddressRepository _addressRepository = addressRepository.ThrowIfNull();
    public async Task<Result<Guid>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = new Address(request.AddressLine, request.AddressType);

        await _addressRepository
            .AddAsync(entity, cancellationToken);

        await _addressRepository
            .SaveChangesAsync(cancellationToken);

        return Result.Ok(entity.Id);
    }
}
