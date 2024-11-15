using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Addresses.Specifications;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Commands.DeleteRange;

internal class DeleteRangeAddressCommandHandler(IAddressRepository addressRepository) : IRequestHandler<DeleteRangeAddressCommand, Result<List<Guid>>>
{
    private readonly IAddressRepository _addressRepository = addressRepository.ThrowIfNull();
    public async Task<Result<List<Guid>>> Handle(DeleteRangeAddressCommand request, CancellationToken cancellationToken)
    {
        var entities = new List<Address>();
        foreach (var addressId in request.Ids)
        {
            var entity = await _addressRepository
                .SingleOrDefaultAsync(new AddressByIdSpecification(addressId), cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Address), addressId);
            }

            entities.Add(entity);
        }

        await _addressRepository.DeleteRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        await _addressRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entities.Select(e => e.Id).ToList());
    }
}
