using PWMS.Domain.Auth.Entities;

namespace PWMS.Application.Auth.Specifications;

public sealed class UserByIdSpecification : Specification<User>, ISingleResultSpecification<User>
{
    public UserByIdSpecification(string id, bool noTracking = false)
    {
        Query.Where(i => i.Id == id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
    }
}
