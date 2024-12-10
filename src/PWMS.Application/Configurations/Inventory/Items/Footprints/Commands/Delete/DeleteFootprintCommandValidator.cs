using PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.Delete;

public class DeleteFootprintCommandValidator : AbstractValidator<DeleteFootprintCommand>
{
    public DeleteFootprintCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotNull();
    }
}
