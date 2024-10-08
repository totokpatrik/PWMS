using MediatR;
using PWMS.Application.Addresses.Repositories;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Commands.CreateAddress;

public sealed class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Guid>
{
    private readonly IAddressRepository _addressRepository;

    public CreateAddressCommandHandler(IAddressRepository repository)
    {
        _addressRepository = repository;
    }

    public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var created = Address.Create(
            request.Name,
            request.EmailAddress,
            request.AddressLine,
            request.Country,
            request.State,
            request.ZipCode);

        await _addressRepository
            .AddAsync(created, cancellationToken)
            .ConfigureAwait(false);

        await _addressRepository
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return created.Id;
    }
}
