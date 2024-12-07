using PWMS.Application.Configuration.Inventory.Items.Items.Models;

namespace PWMS.Application.Configuration.Inventory.Items.Items.Queries.GetById;

public sealed record GetItemByIdQuery(Guid Id) : IRequest<Result<ItemDto>>;