﻿namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Commands.Create;

public sealed class CreateItemFamilyGroupCommandValdator : AbstractValidator<CreateItemFamilyGroupCommand>
{
    public CreateItemFamilyGroupCommandValdator()
    {
        RuleFor(a => a.Name)
            .NotEmpty();
    }
}
