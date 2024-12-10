namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.Delete;

public class DeleteFootprintDetailCommandValidator : AbstractValidator<DeleteFootprintDetailCommand>
{
    public DeleteFootprintDetailCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotNull();
    }
}
