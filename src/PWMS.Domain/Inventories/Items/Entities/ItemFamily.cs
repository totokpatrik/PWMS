using PWMS.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace PWMS.Domain.Inventories.Items.Entities;

public class ItemFamily : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    [ExcludeFromCodeCoverage]
    private ItemFamily() : base(Guid.NewGuid()) { }
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
    [ForeignKey("ItemFamilyGroup")]
    public Guid ItemFamilyGroupId { get; set; }
    public virtual ItemFamilyGroup ItemFamilyGroup { get; set; } = default!;
    public virtual ICollection<Item>? Items { get; set; }
}
