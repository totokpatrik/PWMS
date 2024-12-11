namespace PWMS.Application.Configurations.Inventory.Items.Items.Commands.Create;

public sealed class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        RuleFor(a => a.CreateItemDto.Name)
            .NotEmpty();

        RuleFor(a => a.CreateItemDto.Description)
            .NotEmpty();

        RuleFor(a => a.CreateItemDto.ShortDescription)
            .NotEmpty();
    }
}
