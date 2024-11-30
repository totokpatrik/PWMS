using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Models;
using PWMS.Domain.Addresses.Entities;
using PWMS.Domain.Addresses.Resources;

namespace PWMS.Application.Addresses.Mappings;

public class AddressMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Address, AddressDto>()
            .Map(dest => dest.AddressType, src => src.AddressType.GetDisplayDescription(typeof(AddressResource)));
    }
}
