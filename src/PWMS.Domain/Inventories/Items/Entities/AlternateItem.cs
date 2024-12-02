using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Common;
using PWMS.Domain.Core.Sites.Entities;
using PWMS.Domain.Inventories.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace PWMS.Domain.Inventories.Items.Entities;

public class AlternateItem : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    [ExcludeFromCodeCoverage]
    private AlternateItem() : base(Guid.NewGuid())
    {
    }
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
    [ForeignKey("UnitOfMeasure")]
    public Guid UnitOfMeasureId { get; set; }
    public virtual UnitOfMeasure UnitOfMeasure { get; set; } = default!;
    [ForeignKey("Item")]
    public Guid ItemId { get; set; }
    public virtual Item Item { get; set; } = default!;
}
