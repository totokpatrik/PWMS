using PWMS.Application.Common.Models;

namespace PWMS.Application.Addresses.Models;

public sealed record AddressDto(Guid Id, string AddressLine, string CreatedBy, DateTime Created, string ModifiedBy, DateTime? Modified, DateTime? Deleted, Guid WarehouseId)
    : BaseDto(CreatedBy, Created, ModifiedBy, Modified, Deleted, WarehouseId);