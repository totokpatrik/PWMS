using PWMS.Domain.Common;

namespace PWMS.Domain.Inventories.Items.Entities;

public class ItemFamily : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    public ItemFamily(string name, string description, ItemFamilyGroup itemFamilyGroup) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
        ItemFamilyGroup = itemFamilyGroup;
    }
    public ItemFamily(Guid id, string name, string description, ItemFamilyGroup itemFamilyGroup) : base(id)
    {
        Name = name;
        Description = description;
        ItemFamilyGroup = itemFamilyGroup;
    }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public ItemFamilyGroup ItemFamilyGroup { get; set; } = default!;
}
