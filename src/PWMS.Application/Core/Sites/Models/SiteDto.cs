using PWMS.Application.Common.Models;

namespace PWMS.Application.Core.Sites.Models;

public sealed record SiteDto(Guid Id, string Name, string CreatedBy, DateTime Created, string ModifiedBy, DateTime? Modified, DateTime? Deleted)
    : BaseDto(CreatedBy, Created, ModifiedBy, Modified, Deleted);