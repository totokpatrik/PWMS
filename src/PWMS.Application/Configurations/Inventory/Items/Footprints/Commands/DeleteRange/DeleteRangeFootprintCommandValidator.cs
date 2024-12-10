namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.DeleteRange;

public class DeleteRangeFootprintCommandValidator : AbstractValidator<DeleteRangeFootprintCommand>
{
    public DeleteRangeFootprintCommandValidator()
    {
        RuleFor(a => a.Ids)
            .NotNull();
    }
}
