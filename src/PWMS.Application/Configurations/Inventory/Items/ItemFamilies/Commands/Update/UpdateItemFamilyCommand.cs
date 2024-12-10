using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Models;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Commands.Update;

public sealed record UpdateItemFamilyCommand(Guid Id, string Name, string? Description, Guid ItemFamilyGroupId) : IRequest<Result<ItemFamilyDto>>;