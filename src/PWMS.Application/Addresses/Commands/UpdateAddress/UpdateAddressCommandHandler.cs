using PWMS.Application.Abstractions.Commands;
using PWMS.Application.Abstractions.Repositories;
using PWMS.Domain.Abstractions.Guards;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Commands.UpdateAddress;

public sealed class UpdateAddressCommandHandler : CommandHandler<UpdateAddressCommand>
{
    private readonly IRepository<Address> _addressRepository;

    public UpdateAddressCommandHandler(
        IUnitOfWork unitOfWork,
        IRepository<Address> addressRepository
        ) : base(unitOfWork)
    {
        _addressRepository = addressRepository;
    }

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
    }
}
