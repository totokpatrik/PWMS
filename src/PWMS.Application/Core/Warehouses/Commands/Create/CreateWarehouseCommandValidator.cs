namespace PWMS.Application.Core.Warehouses.Commands.Create;
public sealed class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    public CreateWarehouseCommandValidator()
    {
        RuleFor(a => a.Name)
            .MaximumLength(20)
            .NotEmpty();
    }
}
