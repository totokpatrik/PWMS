using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Addresses.Specifications;

using PWMS.Common.Extensions;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Commands.Delete;

internal class DeleteAddressCommandHandler(IAddressRepository addressRepository) : IRequestHandler<DeleteAddressCommand, Result<Guid>>
{
    private readonly IAddressRepository _addressRepository = addressRepository.ThrowIfNull();
    public async Task<Result<Guid>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await _addressRepository
            .SingleOrDefaultAsync(new AddressByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Address), request.Id);
        }

        await _addressRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
        await _addressRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entity.Id);
    }
}
