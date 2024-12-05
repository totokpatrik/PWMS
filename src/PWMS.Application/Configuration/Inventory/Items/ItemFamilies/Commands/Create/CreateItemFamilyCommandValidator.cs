namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Commands.Create;

public sealed class CreateItemFamilyCommandValidator : AbstractValidator<CreateItemFamilyCommand>
{
    public CreateItemFamilyCommandValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty();

        RuleFor(a => a.ItemFamilyGroupId)
            .NotEmpty();
    }
}
