using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Specifications;

public sealed class AddressByIdSpecification : Specification<Address>, ISingleResultSpecification<Address>
{
    public AddressByIdSpecification(Guid id, bool noTracking = false)
    {
        Query.Where(i => i.Id == id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
    }
}
