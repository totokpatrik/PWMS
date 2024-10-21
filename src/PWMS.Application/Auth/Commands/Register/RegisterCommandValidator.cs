using PWMS.Application.Auth.Commands.Register;

namespace PWMS.Application.Auth.Commands.Login;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(l => l.UserName)
            .NotEmpty();

        RuleFor(l => l.Password)
            .NotEmpty();
    }
}
