using PWMS.Application.Addresses.Commands.Delete;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Commands.Delete;

public class DeleteItemFamilyCommandValidator : AbstractValidator<DeleteAddressCommand>
{
    public DeleteItemFamilyCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotNull();
    }
}
