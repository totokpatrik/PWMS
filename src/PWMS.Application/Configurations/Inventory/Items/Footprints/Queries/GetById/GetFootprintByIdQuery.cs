using PWMS.Application.Configurations.Inventory.Items.Footprints.Models;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Queries.GetById;

public sealed record GetFootprintByIdQuery(Guid Id) : IRequest<Result<FootprintDto>>;