using AutoMapper;
using PWMS.Application.Abstractions.Queries;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Addresses.Repositories;
using PWMS.Domain.Abstractions.Guards;

namespace PWMS.Application.Addresses.Queries.GetAddress;

public sealed class GetAddressQueryHandler : IQueryHandler<GetAddressQuery, AddressDto>
{
    private readonly IAddressRepository _addressRepository;

    public GetAddressQueryHandler(
        IMapper mapper,
        IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public Task<AddressDto> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override async Task<AddressDto> HandleAsync(GetAddressQuery request)
    {
        var address = await _addressRepository.GetByIdAsync(request.Id);
        Guard.Against.NotFound(address);
        return Mapper.Map<AddressDto>(address);
    }
}
