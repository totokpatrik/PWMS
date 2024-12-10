namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Commands.DeleteRange;

public class DeleteRangeItemFamilyGroupCommandValidator : AbstractValidator<DeleteRangeItemFamilyGroupCommand>
{
    public DeleteRangeItemFamilyGroupCommandValidator()
    {
        RuleFor(a => a.Ids)
            .NotNull();
    }
}
