using PWMS.Application.Abstractions.Queries;
using PWMS.Application.Abstractions.Repositories;
using PWMS.Domain.Addresses.Entities;
using Microsoft.EntityFrameworkCore;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Abstractions.Paging;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;
using PWMS.Application.Abstractions.Mapping;
using AutoMapper.QueryableExtensions;

namespace PWMS.Application.Addresses.Queries.GetAddresses;

public class GetAddressesQueryHandler
    : QueryHandler<GetAddressesQuery, PaginatedList<AddressDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Address> _addressRepository;

    public GetAddressesQueryHandler(IMapper mapper, IRepository<Address> addressRepository) : base(mapper)
    {
        _mapper = mapper;
        _addressRepository = addressRepository;
    }

    protected override async Task<PaginatedList<AddressDto>> HandleAsync(GetAddressesQuery request)
    {
        var addressQuery = _addressRepository.GetAll();

        var addresses = await addressQuery
            .OrderBy(a => a.Name)
            .ProjectTo<AddressDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PaginationRequest.PageNumber, request.PaginationRequest.PageSize);

        return addresses;
    }
}
