namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;

public sealed record UpdateFootprintDetailDto(Guid Id,
                                              int UnitQuantity,
                                              int GrossWeight,
                                              int NetWeight,
                                              int Length,
                                              int Width,
                                              int Height);