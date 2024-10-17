using PWMS.Application.Addresses.Models;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Addresses.Specifications;
using PWMS.Application.Common.Exceptions;
using PWMS.Common.Extensions;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Commands.Update;

public sealed class UpdateAddressCommandHandler(IAddressRepository addressRepository) : IRequestHandler<UpdateAddressCommand, Result<AddressDto>>
{
    private readonly IAddressRepository _addressRepository = addressRepository.ThrowIfNull();

    public async Task<Result<AddressDto>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await _addressRepository
            .SingleOrDefaultAsync(new AddressByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Address), request.Id);
        }

        entity.Update(request.AddressLine);

        await _addressRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        var addressDto = entity.Adapt<AddressDto>();

        return Result.Ok(addressDto);
    }
}
