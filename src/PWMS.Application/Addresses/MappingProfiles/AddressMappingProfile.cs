using AutoMapper;
using PWMS.Application.Abstractions.Paging;
using PWMS.Application.Addresses.Models;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.MappingProfiles;

public sealed class AddressMappingProfile : Profile
{
    public AddressMappingProfile()
    {
        CreateMap<Address, AddressDto>();
    }
}
