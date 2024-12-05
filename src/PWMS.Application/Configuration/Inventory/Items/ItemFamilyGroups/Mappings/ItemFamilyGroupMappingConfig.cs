using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Models;
using PWMS.Domain.Inventories.Items.Entities;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Mappings;

public class ItemFamilyGroupMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ItemFamilyGroup, ItemFamilyGroupDto>();
    }
}
