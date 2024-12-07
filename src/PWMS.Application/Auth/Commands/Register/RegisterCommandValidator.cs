namespace PWMS.Application.Auth.Commands.Register;

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
