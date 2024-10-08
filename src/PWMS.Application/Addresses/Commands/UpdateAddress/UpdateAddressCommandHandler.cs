using MediatR;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Addresses.Specifications;
using PWMS.Domain.Abstractions.Guards;

namespace PWMS.Application.Addresses.Commands.UpdateAddress;

public sealed class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand>
{
    private readonly IAddressRepository _addressRepository;

    public UpdateAddressCommandHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }
    /*
    protected async override Task HandleAsync(UpdateAddressCommand request)
    {
        var address = await _addressRepository.GetByIdAsync(request.Id);
        address = Guard.Against.NotFound(address);
        address.Update(
            request.Name,
            request.EmailAddress,
            request.AddressLine,
            request.Country,
            request.State,
            request.ZipCode);

        await UnitOfWork.CommitAsync();
    }*/
    public async Task Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository
            .SingleOrDefaultAsync(new AddressByIdSpecification(request.Id));

        address = Guard.Against.NotFound(address);

        address.Update(
            request.Name,
            request.EmailAddress,
            request.AddressLine,
            request.Country,
            request.State,
            request.ZipCode);

        await _addressRepository
            .SaveChangesAsync(cancellationToken);
    }
}
