using PWMS.Application.Common.Models;

namespace PWMS.Application.Models;

public sealed record AddressDto(Guid Id, string Title, string CreatedBy, DateTime Created, string ModifiedBy, DateTime? Modified, DateTime? Deleted)
    : BaseDto(CreatedBy, Created, ModifiedBy, Modified, Deleted);