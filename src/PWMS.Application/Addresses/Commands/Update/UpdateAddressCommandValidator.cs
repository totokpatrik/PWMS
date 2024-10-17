namespace PWMS.Application.Addresses.Commands.Update;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty();

        RuleFor(a => a.AddressLine)
            .MaximumLength(100)
            .NotEmpty();
    }
}
