namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;

public sealed record CreateFootprintDetailDto(int Level,
                                              Guid UnitOfMeasureId,
                                              int UnitQuantity,
                                              int GrossWeight,
                                              int NetWeight,
                                              int Length,
                                              int Width,
                                              int Height,
                                              Guid FootprintId);
