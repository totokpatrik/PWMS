using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Models;
using PWMS.Domain.Inventories.Items.Entities;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Mappings;

public class ItemFamilyMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ItemFamily, ItemFamilyDto>();
    }
}
