using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Queries.GetById;

public sealed record GetItemFamilyGroupByIdQuery(Guid Id) : IRequest<Result<ItemFamilyGroup>>;