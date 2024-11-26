namespace PWMS.Application.Core.Warehouses.Commands.Select;
public class SelectWarehouseCommandValidator : AbstractValidator<SelectWarehouseCommand>
{
    public SelectWarehouseCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotNull();
    }
}