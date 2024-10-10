using Ardalis.Result;
using MediatR;
using PWMS.Application.Addresses.Repositories;
using PWMS.Core.SharedKernel;
using PWMS.Domain.Addresses.Factories;
using System.Threading;
using System.Threading.Tasks;

namespace PWMS.Application.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Result<CreateAddressResponse>>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAddressCommandHandler(IAddressRepository addressRepository, IUnitOfWork unitOfWork)
    {
        _addressRepository = addressRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateAddressResponse>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = AddressFactory.Create(request.AddressLine);

        _addressRepository.Add(address);

        await _unitOfWork.SaveChangesAsync();

        return Result<CreateAddressResponse>.Success(
            new CreateAddressResponse(address.Id), "Successfully created.");
    }
}
