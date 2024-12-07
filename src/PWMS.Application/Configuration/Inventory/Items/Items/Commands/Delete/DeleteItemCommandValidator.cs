namespace PWMS.Application.Configuration.Inventory.Items.Items.Commands.Delete;

public class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotNull();
    }
}
