using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Models;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Commands.Update;

public sealed record UpdateItemFamilyCommand(Guid Id, string Name, string? Description, Guid ItemFamilyGroupId) : IRequest<Result<ItemFamilyDto>>;