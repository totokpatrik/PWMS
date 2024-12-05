namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Queries.GetById;

internal sealed class GetItemFamilyGroupByIdQueryValidator : AbstractValidator<GetItemFamilyGroupByIdQuery>
{
    public GetItemFamilyGroupByIdQueryValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty();
    }
}
