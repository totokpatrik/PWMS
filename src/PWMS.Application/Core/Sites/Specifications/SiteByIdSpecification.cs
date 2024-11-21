using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Application.Core.Sites.Specifications;

public class SiteByIdSpecification : Specification<Site>, ISingleResultSpecification<Site>
{
    public SiteByIdSpecification(Guid id, bool noTracking = false)
    {
        Query.Where(i => i.Id == id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
    }
}
