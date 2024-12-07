﻿namespace PWMS.Application.Configuration.Inventory.Items.Items.Commands.DeleteRange;

public class DeleteRangeItemCommandValidator : AbstractValidator<DeleteRangeItemCommand>
{
    public DeleteRangeItemCommandValidator()
    {
        RuleFor(a => a.Ids)
            .NotNull();
    }
}
