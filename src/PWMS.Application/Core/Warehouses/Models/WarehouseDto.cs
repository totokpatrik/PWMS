using PWMS.Application.Auth.Models;
using PWMS.Application.Common.Models;

namespace PWMS.Application.Core.Warehouses.Models;

public sealed record WarehouseDto(Guid Id,
                                  string Name,
                                  string CreatedBy,
                                  DateTime Created,
                                  string ModifiedBy,
                                  DateTime? Modified,
                                  DateTime? Deleted,
                                  List<UserDto>? UsersSelected)
    : BaseDto(CreatedBy, Created, ModifiedBy, Modified, Deleted);