using PWMS.Application.Common.Models;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Models;

public sealed record ItemDto(Guid Id,
                             string Name,
                             string Description,
                             string ShortDescription,
                             bool ReceiveStatus,
                             string CreatedBy,
                             DateTime Created,
                             string ModifiedBy,
                             DateTime? Modified,
                             DateTime? Deleted,
                             Guid WarehouseId)
    : BaseWarehouseDto(CreatedBy, Created, ModifiedBy, Modified, Deleted, WarehouseId);