using PWMS.Application.Common.Models;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Models;

public sealed record ItemFamilyDto(Guid Id,
                                string Name,
                                string Description,
                                string CreatedBy,
                                DateTime Created,
                                string ModifiedBy,
                                DateTime? Modified,
                                DateTime? Deleted,
                                Guid WarehouseId)
    : BaseWarehouseDto(CreatedBy, Created, ModifiedBy, Modified, Deleted, WarehouseId);