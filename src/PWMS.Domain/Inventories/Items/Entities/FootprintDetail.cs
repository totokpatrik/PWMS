using PWMS.Domain.Inventories.Entities;

namespace PWMS.Domain.Inventories.Items.Entities;

public class FootprintDetail
{
    public int Level { get; set; }
    public UnitOfMeasure UnitOfMeasure { get; set; } = default!;
    public int UnitQuantity { get; set; }
    public int GrossWeight { get; set; }
    public int NetWeight { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool Receive { get; set; }
}
