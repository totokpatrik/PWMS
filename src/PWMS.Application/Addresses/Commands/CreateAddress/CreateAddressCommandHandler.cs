using Ardalis.Result;
using MediatR;
using PWMS.Application.Addresses.Repositories;
using PWMS.Core.SharedKernel;
using PWMS.Domain.Addresses.Factories;
using System.Threading;
using System.Threading.Tasks;

namespace PWMS.Application.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandler(
    IAddressRepository addressRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateAddressCommand, Result<CreateAddressResponse>>
{
    public async Task<Result<CreateAddressResponse>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {

        // Creating an instance of the address entity.
        var address = AddressFactory.Create(
            request.AddressLine);

        // Adding the entity to the repository.
        addressRepository.Add(address);

        // Saving changes to the database and triggering events.
        await unitOfWork.SaveChangesAsync();

        // Returning the ID and success message.
        return Result<CreateAddressResponse>.Success(
            new CreateAddressResponse(address.Id), "Successfully registered!");
    }
}
