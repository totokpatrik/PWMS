using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Application.Core.Sites.Specifications;

public class SiteCountSpecification : Specification<Site>, ISingleResultSpecification<Site>
{
    public SiteCountSpecification(string currentUserId, bool noTracking = false)
    {
        Query
            .Where(s => s.Owner.Id == currentUserId || s.Admins.Any(a => a.Id == currentUserId) || s.Users.Any(u => u.Id == currentUserId));
    }
}
