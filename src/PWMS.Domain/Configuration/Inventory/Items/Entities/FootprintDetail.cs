using PWMS.Domain.Common;
using PWMS.Domain.Inventories.Entities;
using System.Diagnostics.CodeAnalysis;

namespace PWMS.Domain.Configuration.Inventory.Items.Entities;

public class FootprintDetail : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    [ExcludeFromCodeCoverage]
    private FootprintDetail() : base(Guid.NewGuid()) { }
    public FootprintDetail(
        int level,
        UnitOfMeasure unitOfMeasure,
        int unitQuantity,
        int grossWeight,
        int netWeight,
        int length,
        int width,
        int height,
        bool receive,
        Footprint footprint) : base(Guid.NewGuid())
    {
        Level = level;
        UnitOfMeasure = unitOfMeasure;
        UnitQuantity = unitQuantity;
        GrossWeight = grossWeight;
        NetWeight = netWeight;
        Length = length;
        Width = width;
        Height = height;
        Receive = receive;
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
        bool receive,
        Footprint footprint) : base(id)
    {
        Level = level;
        UnitOfMeasure = unitOfMeasure;
        UnitQuantity = unitQuantity;
        GrossWeight = grossWeight;
        NetWeight = netWeight;
        Length = length;
        Width = width;
        Height = height;
        Receive = receive;
        Footprint = footprint;
    }

    public int Level { get; set; }
    public int UnitQuantity { get; set; }
    public int GrossWeight { get; set; }
    public int NetWeight { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool Receive { get; set; }
    [ForeignKey("Footprint")]
    public Guid FoorptinrId { get; set; }
    public virtual Footprint Footprint { get; set; } = default!;
    [ForeignKey("UnitOfMeasure")]
    public Guid UnitOfMeasureId { get; set; }
    public virtual UnitOfMeasure UnitOfMeasure { get; set; } = default!;
}
