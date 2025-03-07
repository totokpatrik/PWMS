﻿namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.Create;

public sealed class CreateFootprintCommandValidator : AbstractValidator<CreateFootprintCommand>
{
    public CreateFootprintCommandValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty();
    }
}
