using PWMS.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace PWMS.Domain.Configuration.Inventory.Items.Entities;

public class ItemFamily : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    [ExcludeFromCodeCoverage]
    private ItemFamily() : base(Guid.NewGuid()) { }
    public ItemFamily(string name, string? description, ItemFamilyGroup itemFamilyGroup) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
        ItemFamilyGroup = itemFamilyGroup;
    }
    public ItemFamily(Guid id, string name, string? description, ItemFamilyGroup itemFamilyGroup) : base(id)
    {
        Name = name;
        Description = description;
        ItemFamilyGroup = itemFamilyGroup;
    }

    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    [ForeignKey("ItemFamilyGroup")]
    public Guid ItemFamilyGroupId { get; set; }
    public virtual ItemFamilyGroup ItemFamilyGroup { get; set; } = default!;
    public virtual ICollection<Item>? Items { get; set; }
    public void Update(string name, string? description, ItemFamilyGroup itemFamilyGroup)
    {
        Name = name;
        Description = description;
        ItemFamilyGroup = itemFamilyGroup;
    }
}
