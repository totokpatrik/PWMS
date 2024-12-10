namespace PWMS.Application.Configurations.Inventory.Items.Items.Commands.Create;

public sealed class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty();

        RuleFor(a => a.Description)
            .NotEmpty();

        RuleFor(a => a.ShortDescription)
            .NotEmpty();

        RuleFor(a => a.ItemFamilyId)
            .NotEmpty();
    }
}
