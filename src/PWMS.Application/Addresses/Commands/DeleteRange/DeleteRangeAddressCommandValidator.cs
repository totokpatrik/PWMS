namespace PWMS.Application.Addresses.Commands.DeleteRange;

public class DeleteRangeAddressCommandValidator : AbstractValidator<DeleteRangeAddressCommand>
{
    public DeleteRangeAddressCommandValidator()
    {
        RuleFor(a => a.Ids)
            .NotNull();
    }
}
