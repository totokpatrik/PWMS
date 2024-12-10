using PWMS.Domain.Common;
using PWMS.Domain.Inventories.Entities;
using System.Diagnostics.CodeAnalysis;

namespace PWMS.Domain.Configuration.Inventory.Items.Entities;

public class FootprintDetail : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    [ExcludeFromCodeCoverage]
    private FootprintDetail() : base(Guid.NewGuid()) { }
    public FootprintDetail(
        UnitOfMeasure unitOfMeasure,
        int unitQuantity,
        int grossWeight,
        int netWeight,
        int length,
        int width,
        int height,
        Footprint footprint) : base(Guid.NewGuid())
    {
        UnitOfMeasure = unitOfMeasure;
        UnitQuantity = unitQuantity;
        GrossWeight = grossWeight;
        NetWeight = netWeight;
        Length = length;
        Width = width;
        Height = height;
        Footprint = footprint;
    }

    public FootprintDetail(
        Guid id,
        int level,
        UnitOfMeasure unitOfMeasure,
        int unitQuantity,
        int grossWeight,
        int netWeight,
        int length,
        int width,
        int height,
        Footprint footprint) : base(id)
    {
        UnitOfMeasure = unitOfMeasure;
        UnitQuantity = unitQuantity;
        GrossWeight = grossWeight;
        NetWeight = netWeight;
        Length = length;
        Width = width;
        Height = height;
        Footprint = footprint;
    }
    public int UnitQuantity { get; set; }
    public int GrossWeight { get; set; }
    public int NetWeight { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    [ForeignKey("Footprint")]
    public Guid FootprintId { get; set; }
    public virtual Footprint Footprint { get; set; } = default!;
    [ForeignKey("UnitOfMeasure")]
    public Guid UnitOfMeasureId { get; set; }
    public virtual UnitOfMeasure UnitOfMeasure { get; set; } = default!;
    public void Update(int unitQuantity, int grossWeight, int netWeight, int length, int width, int height)
    {
        UnitQuantity = unitQuantity;
        GrossWeight = grossWeight;
        NetWeight = netWeight;
        Length = length;
        Width = width;
        Height = height;
    }
}
