using PWMS.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace PWMS.Domain.Configuration.Inventory.Items.Entities;

public class Footprint : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    [ExcludeFromCodeCoverage]
    private Footprint() : base(Guid.NewGuid()) { }
    public Footprint(string name, bool @default, Item item) : base(Guid.NewGuid())
    {
        Name = name;
        Default = @default;
        Item = item;
    }

    public Footprint(Guid id, string name, bool @default, Item item) : base(id)
    {
        Name = name;
        Default = @default;
        Item = item;
    }
    public string Name { get; set; } = default!;
    public bool Default { get; set; }
    [ForeignKey("Item")]
    public Guid ItemId { get; set; }
    public virtual Item Item { get; set; } = default!;
    public List<FootprintDetail>? FootprintDetails { get; set; }

    public void Update(string name, bool @default, Guid itemId)
    {
        Name = name;
        Default = @default;
        ItemId = itemId;
    }
}
