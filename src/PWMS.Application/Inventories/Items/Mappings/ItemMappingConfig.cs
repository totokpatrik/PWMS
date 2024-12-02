using PWMS.Application.Addresses.Models;
using PWMS.Application.Inventories.Items.Models;
using PWMS.Domain.Addresses.Entities;
using PWMS.Domain.Addresses.Resources;
using PWMS.Domain.Inventories.Items.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Inventories.Items.Mappings;

public class ItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Item, ItemDto>();
    }
}
