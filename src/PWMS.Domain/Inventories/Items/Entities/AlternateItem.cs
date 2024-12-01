using PWMS.Domain.Common;
using PWMS.Domain.Inventories.Entities;

namespace PWMS.Domain.Inventories.Items.Entities;

public class AlternateItem : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    public AlternateItem(string value, AlternateItemType alternateItemType, UnitOfMeasure unitOfMeasure) : base(Guid.NewGuid())
    {
        Value = value;
        AlternateItemType = alternateItemType;
        UnitOfMeasure = unitOfMeasure;
    }

    public AlternateItem(Guid id, string value, AlternateItemType alternateItemType, UnitOfMeasure unitOfMeasure) : base(id)
    {
        Value = value;
        AlternateItemType = alternateItemType;
        UnitOfMeasure = unitOfMeasure;
    }

    public string Value { get; set; } = default!;
    public AlternateItemType AlternateItemType { get; set; } = default!;
    public UnitOfMeasure UnitOfMeasure { get; set; } = default!;
}
