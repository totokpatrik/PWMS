namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Models;

public sealed record CreateFootprintDto(string Name, bool Default, Guid ItemId);