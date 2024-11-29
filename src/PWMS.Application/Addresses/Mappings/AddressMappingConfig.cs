using PWMS.Application.Addresses.Models;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Mappings;

public class AddressMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Address, AddressDto>()
            .Map(dest => dest.AddressType, src => "asd");
    }
}
