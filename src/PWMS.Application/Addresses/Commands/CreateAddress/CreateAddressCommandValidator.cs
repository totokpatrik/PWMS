using FluentValidation;

namespace PWMS.Application.Addresses.Commands.CreateAddress;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(command => command.AddressLine)
            .NotEmpty()
            .MaximumLength(100);
    }
}
