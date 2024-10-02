using FluentValidation;

namespace PWMS.Application.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
{
    public DeleteAddressCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.");
    }
}
