namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Commands.Delete;

public class DeleteItemFamilyGroupCommandValidator : AbstractValidator<DeleteItemFamilyGroupCommand>
{
    public DeleteItemFamilyGroupCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotNull();
    }
}
