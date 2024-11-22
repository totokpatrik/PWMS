using PWMS.Application.Auth.Models;
using PWMS.Domain.Auth.Entities;

namespace PWMS.Application.Auth.Mappings;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>();
    }
}
