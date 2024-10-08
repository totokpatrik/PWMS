using MediatR;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Addresses.Specifications;
using PWMS.Domain.Abstractions.Guards;

namespace PWMS.Application.Addresses.Commands.DeleteAddress;

public sealed class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand>
{
    private readonly IAddressRepository _addressRepository;

    public DeleteAddressCommandHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository
            .FirstOrDefaultAsync(new AddressByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        address = Guard.Against.NotFound(address);

        await _addressRepository
            .DeleteAsync(address, cancellationToken);

        await _addressRepository
            .SaveChangesAsync(cancellationToken);
    }
}
