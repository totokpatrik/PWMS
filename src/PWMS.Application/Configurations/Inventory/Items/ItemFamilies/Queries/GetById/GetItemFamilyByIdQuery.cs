using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Models;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Queries.GetById;
public sealed record GetItemFamilyByIdQuery(Guid Id) : IRequest<Result<ItemFamilyDto>>;