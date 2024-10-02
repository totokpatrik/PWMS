using PWMS.Application.Abstractions.Commands;
using PWMS.Application.Abstractions.Repositories;
using PWMS.Domain.Abstractions.Guards;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Commands.DeleteAddress;

public sealed class DeleteAddressCommandHandler : CommandHandler<DeleteAddressCommand>
{
    private readonly IRepository<Address> _addressRepository;

    public DeleteAddressCommandHandler(
        IUnitOfWork unitOfWork,
        IRepository<Address> addressRepository
        ) : base(unitOfWork)
    {
        _addressRepository = addressRepository;
    }

    protected override async Task HandleAsync(DeleteAddressCommand request)
    {
        var address = await _addressRepository.GetByIdAsync(request.Id);

        address = Guard.Against.NotFound(address);

        _addressRepository.Delete(address);

        await UnitOfWork.CommitAsync();
    }
}
