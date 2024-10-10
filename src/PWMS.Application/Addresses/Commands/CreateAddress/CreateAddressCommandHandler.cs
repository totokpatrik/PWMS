using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using PWMS.Application.Addresses.Repositories;
using PWMS.Core.SharedKernel;
using PWMS.Domain.Addresses.Factories;
using System.Threading;
using System.Threading.Tasks;

namespace PWMS.Application.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandler(
    IValidator<CreateAddressCommand> validator,
    IAddressRepository addressRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateAddressCommand, Result<CreateAddressResponse>>
{
    public async Task<Result<CreateAddressResponse>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Return the result with validation errors.
            return Result<CreateAddressResponse>.Invalid(validationResult.AsErrors());
        }

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
