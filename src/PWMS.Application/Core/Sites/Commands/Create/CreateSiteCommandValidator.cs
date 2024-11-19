namespace PWMS.Application.Core.Sites.Commands.Create;

public sealed class CreateSiteCommandValidator : AbstractValidator<CreateSiteCommand>
{
    public CreateSiteCommandValidator()
    {
        RuleFor(a => a.Name)
            .MaximumLength(20)
            .NotEmpty();
    }
}
