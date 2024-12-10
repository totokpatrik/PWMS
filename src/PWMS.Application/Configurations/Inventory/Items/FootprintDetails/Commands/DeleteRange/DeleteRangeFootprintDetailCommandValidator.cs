namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.DeleteRange;

public class DeleteRangeFootprintDetailCommandValidator : AbstractValidator<DeleteRangeFootprintDetailCommand>
{
    public DeleteRangeFootprintDetailCommandValidator()
    {
        RuleFor(a => a.Ids)
            .NotNull();
    }
}
