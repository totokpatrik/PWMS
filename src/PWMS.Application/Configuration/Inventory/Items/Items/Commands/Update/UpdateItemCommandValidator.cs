namespace PWMS.Application.Configuration.Inventory.Items.Items.Commands.Update;

public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty();

        RuleFor(a => a.Name)
            .NotEmpty();

        RuleFor(a => a.Description)
            .NotEmpty();

        RuleFor(a => a.ShortDescription)
            .NotEmpty();
    }
}
