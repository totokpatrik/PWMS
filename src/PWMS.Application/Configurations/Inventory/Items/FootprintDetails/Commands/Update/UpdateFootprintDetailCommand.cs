using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Commands.Update;

public sealed record UpdateFootprintDetailCommand(UpdateFootprintDetailDto Update) : IRequest<Result<FootprintDetailDto>>;