using PWMS.Application.Common.CQRS;
using PWMS.Application.Common.Exceptions;
using PWMS.Application.Common.Interfaces;

namespace PWMS.Application.Addresses.Queries.GetAddresses;

public class GetAddressesQueryHandler(IAddressRepository _addressRepository)
    : IQueryHandler<GetAddressesQuery, GetAddressesResult>
{
    public async Task<GetAddressesResult> Handle(GetAddressesQuery query, CancellationToken cancellationToken)
    {
        var addresses = await _addressRepository.GetAsync(query.PaginationRequest, cancellationToken);

        if (addresses.Data.Count() == 0)
        {
            throw new NotFoundException("Address list not found");
        }

        return new GetAddressesResult(addresses);
    }
}
