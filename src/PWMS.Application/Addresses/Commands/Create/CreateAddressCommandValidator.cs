namespace PWMS.Application.Addresses.Commands.Create;

public sealed class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(a => a.AddressLine)
            .MaximumLength(100)
            .NotEmpty();
    }
}
