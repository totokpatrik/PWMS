using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Application.Core.Sites.Specifications;

public class SiteByIdSpecification : Specification<Site>, ISingleResultSpecification<Site>
{
    public SiteByIdSpecification(Guid id, string currentUserId, bool noTracking = false)
    {
        Query.Where(i => i.Id == id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
        Query
            .Where(s => s.Owner.Id == currentUserId || s.Admins.Any(a => a.Id == currentUserId) || s.Users.Any(u => u.Id == currentUserId));
    }
}
