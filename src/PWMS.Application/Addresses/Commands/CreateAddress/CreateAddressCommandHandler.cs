using PWMS.Application.Abstractions.Commands;
using PWMS.Application.Abstractions.Repositories;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Commands.CreateAddress;

public sealed class CreateAddressCommandHandler : CreateCommandHandler<CreateAddressCommand>
{
    private readonly IRepository<Address> _addressRepository;
    public CreateAddressCommandHandler(
        IUnitOfWork unitOfWork,
        IRepository<Address> addressRepository
        ) : base(unitOfWork)
    {
        _addressRepository = addressRepository;
    }
    protected override async Task<Guid> HandleAsync(CreateAddressCommand request)
    {
        var created = Address.Create(
            request.Name,
            request.EmailAddress,
            request.AddressLine,
            request.Country,
            request.State,
            request.ZipCode);

        _addressRepository.Insert(created);
        await UnitOfWork.CommitAsync();

        return created.Id;
    }
}
