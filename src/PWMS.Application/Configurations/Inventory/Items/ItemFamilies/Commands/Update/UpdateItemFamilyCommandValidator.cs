namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Commands.Update;
public class UpdateItemFamilyCommandValidator : AbstractValidator<UpdateItemFamilyCommand>
{
    public UpdateItemFamilyCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty();

        RuleFor(a => a.Name)
            .NotEmpty();
    }
}
