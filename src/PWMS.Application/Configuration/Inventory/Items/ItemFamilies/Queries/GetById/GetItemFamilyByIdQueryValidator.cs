namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Queries.GetById;

internal sealed class GetItemFamilyByIdQueryValidator : AbstractValidator<GetItemFamilyByIdQuery>
{
    public GetItemFamilyByIdQueryValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty();
    }
}
