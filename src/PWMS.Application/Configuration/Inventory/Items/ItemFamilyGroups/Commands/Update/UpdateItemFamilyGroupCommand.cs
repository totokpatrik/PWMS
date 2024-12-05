using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Models;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Commands.Update;

public sealed record UpdateItemFamilyGroupCommand(Guid Id, string Name, string Description) : IRequest<Result<ItemFamilyGroupDto>>;