namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Queries.GetById;

internal sealed class GetFootprintByIdValidator : AbstractValidator<GetFootprintByIdQuery>
{
    public GetFootprintByIdValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty();
    }
}
