using PWMS.Application.Configurations.Inventory.Items.Items.Models;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Queries.GetById;

public sealed record GetItemByIdQuery(Guid Id) : IRequest<Result<ItemDto>>;