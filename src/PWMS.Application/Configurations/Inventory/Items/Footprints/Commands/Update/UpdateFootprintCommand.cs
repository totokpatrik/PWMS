using PWMS.Application.Configurations.Inventory.Items.Footprints.Models;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Commands.Update;
public sealed record UpdateFootprintCommand(Guid Id, string Name, bool Default) : IRequest<Result<FootprintDto>>;