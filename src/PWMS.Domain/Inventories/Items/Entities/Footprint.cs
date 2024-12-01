using PWMS.Domain.Common;

namespace PWMS.Domain.Inventories.Items.Entities;

public class Footprint : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    public Footprint(string name, string description, bool @default, Item item) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
        Default = @default;
        Item = item;
    }

    public Footprint(Guid id, string name, string description, bool @default, Item item) : base(id)
    {
        Name = name;
        Description = description;
        Default = @default;
        Item = item;
    }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool Default { get; set; } = false;
    public Item Item { get; set; } = default!;
    public List<FootprintDetail>? FootprintDetails { get; set; }

}
