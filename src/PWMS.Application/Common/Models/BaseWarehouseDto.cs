namespace PWMS.Application.Common.Models;

public record BaseWarehouseDto(string CreatedBy, DateTime Created, string ModifiedBy, DateTime? Modified, DateTime? Deleted, Guid WarehouseId);