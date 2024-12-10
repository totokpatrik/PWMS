using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.Create;

public sealed record CreateFootprintDetailCommand(CreateFootprintDetailDto CreateFootprintDetailDto) : IRequest<Result<Guid>>;