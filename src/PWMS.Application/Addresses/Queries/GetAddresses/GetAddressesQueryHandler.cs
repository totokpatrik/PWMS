using PWMS.Application.Common.CQRS;
using PWMS.Application.Common.Exceptions;

namespace PWMS.Application.Addresses.Queries.GetAddresses;

public class GetAddressesQueryHandler(IAddressesRepository _addressesRepository)
    : IQueryHandler<GetAddressesQuery, GetAddressesResult>
{
    public async Task<GetAddressesResult> Handle(GetAddressesQuery query, CancellationToken cancellationToken)
    {
        var addresses = await _addressesRepository.GetAsync(query.PaginationRequest, cancellationToken);

        if (addresses.Data.Count() == 0)
        {
            throw new NotFoundException("Address list not found");
        }

        return new GetAddressesResult(addresses);
    }
}
