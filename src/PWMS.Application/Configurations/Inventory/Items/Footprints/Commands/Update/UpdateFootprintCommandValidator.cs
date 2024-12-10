namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.Update;

public class UpdateFootprintCommandValidator : AbstractValidator<UpdateFootprintCommand>
{
    public UpdateFootprintCommandValidator()
    {
        RuleFor(fp => fp.Id)
            .NotEmpty();

        RuleFor(fp => fp.Name)
            .NotEmpty();
    }
}
