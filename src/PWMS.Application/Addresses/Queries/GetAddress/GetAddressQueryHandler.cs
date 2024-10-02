using AutoMapper;
using PWMS.Application.Abstractions.Queries;
using PWMS.Application.Abstractions.Repositories;
using PWMS.Application.Addresses.Models;
using PWMS.Domain.Abstractions.Guards;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Queries.GetAddress;

public sealed class GetAddressQueryHandler : QueryHandler<GetAddressQuery, AddressDto>
{
    private readonly IRepository<Address> _addressRepository;

    public GetAddressQueryHandler(
        IMapper mapper,
        IRepository<Address> addressRepository) : base(mapper)
    {
        _addressRepository = addressRepository;
    }

    protected override async Task<AddressDto> HandleAsync(GetAddressQuery request)
    {
        var address = await _addressRepository.GetByIdAsync(request.Id);
        Guard.Against.NotFound(address);
        return Mapper.Map<AddressDto>(address);
    }
}
