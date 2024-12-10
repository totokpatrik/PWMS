using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Models;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Mappings;

public class ItemFamilyGroupMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ItemFamilyGroup, ItemFamilyGroupDto>();
    }
}
