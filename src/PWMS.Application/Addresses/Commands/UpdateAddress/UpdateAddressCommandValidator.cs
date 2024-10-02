using FluentValidation;

namespace PWMS.Application.Addresses.Commands.UpdateAddress;

public sealed class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.AddressLine).NotEmpty().WithMessage("Address line is required");
        RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");
        RuleFor(x => x.State).NotEmpty().WithMessage("State is required");
        RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Zip code is required");
    }
}
