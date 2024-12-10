namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Commands.DeleteRange;

public class DeleteRangeItemFamilyCommandValidator : AbstractValidator<DeleteRangeItemFamilyCommand>
{
    public DeleteRangeItemFamilyCommandValidator()
    {
        RuleFor(a => a.Ids)
            .NotNull();
    }
}
