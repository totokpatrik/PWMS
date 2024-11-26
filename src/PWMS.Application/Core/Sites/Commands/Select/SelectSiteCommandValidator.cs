namespace PWMS.Application.Core.Sites.Commands.Select;

public class SelectSiteCommandValidator : AbstractValidator<SelectSiteCommand>
{
    public SelectSiteCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotNull();
    }
}