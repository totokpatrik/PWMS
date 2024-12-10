using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Queries.GetById;

public sealed record GetFootprintDetailByIdQuery(Guid Id) : IRequest<Result<FootprintDetailDto>>;