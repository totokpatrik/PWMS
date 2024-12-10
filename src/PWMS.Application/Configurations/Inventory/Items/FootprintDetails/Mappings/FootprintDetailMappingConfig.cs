using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Mappings;

public class FootprintDetailMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FootprintDetail, FootprintDetailDto>();
    }
}
