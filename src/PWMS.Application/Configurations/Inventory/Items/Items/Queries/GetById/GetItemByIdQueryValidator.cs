﻿namespace PWMS.Application.Configurations.Inventory.Items.Items.Queries.GetById;

internal sealed class GetItemByIdQueryValidator : AbstractValidator<GetItemByIdQuery>
{
    public GetItemByIdQueryValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty();
    }
}
