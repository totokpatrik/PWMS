using PWMS.Domain.Common;
using PWMS.Domain.Inventories.Items.Entities;

namespace PWMS.Domain.Inventories.Entities;

public class UnitOfMeasure : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    public UnitOfMeasure(string name, string description) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
    }

    public UnitOfMeasure(Guid id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public ICollection<FootprintDetail>? FootprintDetails { get; set; }
    public ICollection<AlternateItem>? AlternateItems { get; set; }
}
