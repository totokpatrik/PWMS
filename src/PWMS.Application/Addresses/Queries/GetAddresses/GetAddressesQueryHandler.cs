using PWMS.Application.Abstractions.Queries;
using PWMS.Application.Abstractions.Repositories;
using PWMS.Domain.Addresses.Entities;
using Microsoft.EntityFrameworkCore;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Abstractions.Paging;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;

namespace PWMS.Application.Addresses.Queries.GetAddresses;

public class GetAddressesQueryHandler
    : QueryHandler<GetAddressesQuery, PaginatedResult<AddressDto>>
{
    private readonly IRepository<Address> _addressRepository;

    public GetAddressesQueryHandler(IMapper mapper, IRepository<Address> addressRepository) : base(mapper)
    {
        _addressRepository = addressRepository;
    }

    protected override async Task<PaginatedResult<AddressDto>> HandleAsync(GetAddressesQuery request)
    {
        var addressQuery = _addressRepository.GetAll();

        var addresses = await addressQuery
            .OrderBy(a => a.Name)
            .ToListAsync();

        return Mapper.Map<PaginatedResult<AddressDto>>(addresses);
    }
}
