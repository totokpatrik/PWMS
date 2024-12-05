using PWMS.Application.Common.Models;

namespace PWMS.Application.Configuration.Inventory.Items.Footprints.Models;
public sealed record FootprintDto(Guid Id,
                                string Name,
                                bool Default,
                                string CreatedBy,
                                DateTime Created,
                                string ModifiedBy,
                                DateTime? Modified,
                                DateTime? Deleted,
                                Guid WarehouseId)
    : BaseWarehouseDto(CreatedBy, Created, ModifiedBy, Modified, Deleted, WarehouseId);