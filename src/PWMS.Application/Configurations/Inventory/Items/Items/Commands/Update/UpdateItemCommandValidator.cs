namespace PWMS.Application.Configurations.Inventory.Items.Items.Commands.Update;

public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(a => a.UpdateItemDto.Id)
            .NotEmpty();

        RuleFor(a => a.UpdateItemDto.Name)
            .NotEmpty();

        RuleFor(a => a.UpdateItemDto.Description)
            .NotEmpty();

        RuleFor(a => a.UpdateItemDto.ShortDescription)
            .NotEmpty();
    }
}
