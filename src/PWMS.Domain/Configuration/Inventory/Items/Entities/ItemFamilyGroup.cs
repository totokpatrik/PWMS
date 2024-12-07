using PWMS.Domain.Common;

namespace PWMS.Domain.Configuration.Inventory.Items.Entities;

public class ItemFamilyGroup : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    public ItemFamilyGroup(string name, string description) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
    }

    public ItemFamilyGroup(Guid id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public ICollection<ItemFamily>? ItemFamilies { get; set; }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
