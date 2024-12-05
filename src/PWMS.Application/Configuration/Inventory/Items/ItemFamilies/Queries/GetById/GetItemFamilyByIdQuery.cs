using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Models;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Queries.GetById;
public sealed record GetItemFamilyByIdQuery(Guid Id) : IRequest<Result<ItemFamilyDto>>;