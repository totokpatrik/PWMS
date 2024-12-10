namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Queries.GetById;

internal sealed class GetFootprintDetailByIdValidator : AbstractValidator<GetFootprintDetailByIdQuery>
{
    public GetFootprintDetailByIdValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty();
    }
}
