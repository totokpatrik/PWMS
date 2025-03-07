﻿using PWMS.Application.Common.Models;

namespace PWMS.Application.Addresses.Models;

public sealed record AddressDto(Guid Id,
                                string AddressLine,
                                string AddressType,
                                string CreatedBy,
                                DateTime Created,
                                string ModifiedBy,
                                DateTime? Modified,
                                DateTime? Deleted,
                                Guid WarehouseId)
    : BaseWarehouseDto(CreatedBy, Created, ModifiedBy, Modified, Deleted, WarehouseId);