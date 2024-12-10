using PWMS.Application.Common.Models;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;

public sealed record FootprintDetailDto(Guid Id,
                            int Level,
                            Guid UnitOfMeasureId,
                            int UnitQuantity,
                            int GrossWeight,
                            int NetWeight,
                            int Length,
                            int Width,
                            int Height,
                            Guid FootprintId,
                            string CreatedBy,
                            DateTime Created,
                            string ModifiedBy,
                            DateTime? Modified,
                            DateTime? Deleted,
                            Guid WarehouseId)
: BaseWarehouseDto(CreatedBy, Created, ModifiedBy, Modified, Deleted, WarehouseId);