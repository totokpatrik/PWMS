namespace PWMS.Application.Addresses.Commands.Delete;

public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
{
    public DeleteAddressCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotNull();
    }
}
